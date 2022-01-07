using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Model.RabbitMQMessages;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Pharmacy.Services
{
    public class NewTenderRabbitMQService : BackgroundService
    {
        private IConnection _connection;
        private List<IModel> _channels;
        private List<string> _queueNames;
        private readonly IUnitOfWork _uow;

        public NewTenderRabbitMQService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _channels = new List<IModel>();
            _queueNames = new List<string>();
            InitRabbitMQ();
        }
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };

            _connection = factory.CreateConnection();
            var hospitals = _uow.GetRepository<IHospitalReadRepository>().GetAll();
            foreach (var hospital in hospitals)
            {
                var channel = _connection.CreateModel();
                channel.ExchangeDeclare("new tender", ExchangeType.Direct);
                var queueName = channel.QueueDeclare().QueueName;
                _queueNames.Add(queueName);
                channel.QueueBind(queueName, "new tender", hospital.ApiKey.ToString());
                channel.BasicQos(0, 1, false);
                _channels.Add(channel);
                Debug.WriteLine("Poruka");
            }
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<EventingBasicConsumer> consumers = new List<EventingBasicConsumer>();
            foreach(var iteration in _channels.Zip(_queueNames, Tuple.Create))
            {
                var consumer = new EventingBasicConsumer(iteration.Item1);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    Debug.WriteLine(jsonMessage);
                    var newTender = JsonConvert.DeserializeObject<NewTenderMessage>(jsonMessage);
                    var hospital = _uow.GetRepository<IHospitalReadRepository>()
                        .GetAll().FirstOrDefault(h => h.ApiKey == newTender.Apikey);
                    if (hospital == null) 
                    {
                        iteration.Item1.BasicAck(ea.DeliveryTag, false);
                        return;
                    }
                    Tender tender = new Tender
                    {
                        ActiveRange = new TimeRange(newTender.StartDate, newTender.EndDate),
                        ClosedDate = DateTime.MinValue,
                        CreatedDate = newTender.CreatedDate,
                        HospitalId = hospital.Id,
                        MedicationRequests = new List<MedicationRequest>(),
                        Name = newTender.Name
                    };
                    foreach (var req in newTender.MedicationRequestDto)
                    {
                        tender.MedicationRequests.Add(new MedicationRequest(req.MedicineName, req.Quantity));
                    }

                    _uow.GetRepository<ITenderWriteRepository>().Add(tender);
                    iteration.Item1.BasicAck(ea.DeliveryTag, false);
                };
                consumer.Shutdown += OnConsumerShutdown;
                consumer.Registered += OnConsumerRegistered;
                consumer.Unregistered += OnConsumerUnregistered;
                consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

                iteration.Item1.BasicConsume(queue: iteration.Item2,
                    autoAck: false,
                    consumer: consumer);

                consumers.Add(consumer);
            }
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            foreach (var channel in _channels)
            {
                channel.Close();
            }
            _connection.Close();
            base.Dispose();
        }
    }
}
