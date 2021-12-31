using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;
using Integration.Tendering.Model.RabbitMQMessages;
using Integration.Tendering.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Integration.Tendering.Service
{
    public class NewTenderOfferRabbitMQService : BackgroundService
    {
        private IConnection _connection;
        private List<IModel> _channels;
        private List<string> _queueNames;
        private readonly IUnitOfWork _uow;

        public NewTenderOfferRabbitMQService(IUnitOfWork unitOfWork)
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
            var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll();
            foreach (var pharmacy in pharmacies)
            {
                var channel = _connection.CreateModel();
                channel.ExchangeDeclare("new tender offer", ExchangeType.Direct);
                var queueName = channel.QueueDeclare().QueueName;
                _queueNames.Add(queueName);
                channel.QueueBind(queueName, "new tender offer", pharmacy.ApiKey.ToString());
                channel.BasicQos(0, 1, false);
                _channels.Add(channel);
                Debug.WriteLine("Poruka");
            }
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<EventingBasicConsumer> consumers = new List<EventingBasicConsumer>();
            foreach (var iteration in _channels.Zip(_queueNames, Tuple.Create))
            {
                var consumer = new EventingBasicConsumer(iteration.Item1);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    Debug.WriteLine(jsonMessage);
                    var newTenderOffer = JsonConvert.DeserializeObject<NewTenderOfferMessage>(jsonMessage);
                    var pharmacy = _uow.GetRepository<IPharmacyReadRepository>()
                        .GetAll().FirstOrDefault(p => p.ApiKey == newTenderOffer.Apikey);
                    if (pharmacy == null) return;
                    TenderOffer tenderOffer = new TenderOffer(pharmacy,
                        new Money(newTenderOffer.Cost, newTenderOffer.Currency), newTenderOffer.CreatedDate);
                    foreach (MedicationRequestMessage medReq in newTenderOffer.MedicationRequests)
                    {
                        tenderOffer.AddMedicationRequest(new MedicationRequest(medReq.MedicineName, medReq.Quantity));
                    }
                    var tender = _uow.GetRepository<ITenderReadRepository>().GetAll().Include(t => t.TenderOffers)
                        .FirstOrDefault(t => t.CreatedTime == newTenderOffer.TenderCreatedDate);
                    if (tender == null) return;
                    tender.AddTenderOffer(tenderOffer);
                    _uow.GetRepository<ITenderWriteRepository>().Update(tender);
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
