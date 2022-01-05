using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
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
        private readonly List<IModel> _channels;
        private readonly List<string> _queueNames;
        private readonly IUnitOfWork _uow;
        private List<int> _pharmacyIds;
        private Thread _thread;
        private readonly List<EventingBasicConsumer> consumers;
        private bool _runThread;

        public NewTenderOfferRabbitMQService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _channels = new List<IModel>();
            _queueNames = new List<string>();
            _pharmacyIds = new List<int>();
            consumers = new List<EventingBasicConsumer>();
            _runThread = true;
            InitRabbitMQ();
            _thread = new Thread(ChannelThreads);
            _thread.Start();
        }

        private void ChannelThreads()
        {
            while (_runThread)
            {
                var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
                foreach (var pharmacy in pharmacies)
                {
                    if (!_pharmacyIds.Contains(pharmacy.Id))
                    {
                        CreateChannel(pharmacy);
                        CreateExecuteAsync(new Tuple<IModel, string>(_channels.Last(), _queueNames.Last()));
                        Debug.WriteLine("New pharmacy made");
                    }
                }

                Debug.WriteLine(_pharmacyIds.Count);
                Debug.WriteLine("Channel making thread goes to sleep");
                Thread.Sleep(10000);
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) 
        { 
            //no action needed for "consumer cancelled"
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            //no action needed for "consumer unregistered"
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            //no action needed for "consumer registered"
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            //no action needed for "consumer shutdown"
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            //no action needed for "connection shutdown"
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };

            _connection = factory.CreateConnection();
            var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll();
            foreach (var pharmacy in pharmacies)
            {
                CreateChannel(pharmacy);
            }
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void CreateChannel(Pharmacy pharmacy)
        {
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare("new tender offer", ExchangeType.Direct);
            var queueName = channel.QueueDeclare().QueueName;
            _queueNames.Add(queueName);
            channel.QueueBind(queueName, "new tender offer", pharmacy.ApiKey.ToString());
            channel.BasicQos(0, 1, false);
            _channels.Add(channel);
            Debug.WriteLine("Poruka");
            _pharmacyIds.Add(pharmacy.Id);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var iteration in _channels.Zip(_queueNames, Tuple.Create))
            {
                CreateExecuteAsync(iteration);
            }
            return Task.CompletedTask;
        }

        private void CreateExecuteAsync(Tuple<IModel, string> iteration)
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
                if (pharmacy == null)
                {
                    iteration.Item1.BasicAck(ea.DeliveryTag, false);
                    return;
                }

                TenderOffer tenderOffer = new TenderOffer(pharmacy,
                    new Money(newTenderOffer.Cost, newTenderOffer.Currency), newTenderOffer.CreatedDate);
                foreach (MedicationRequestMessage medReq in newTenderOffer.MedicationRequests)
                {
                    tenderOffer.AddMedicationRequest(new MedicationRequest(medReq.MedicineName, medReq.Quantity));
                }

                var tender = _uow.GetRepository<ITenderReadRepository>().GetAll().Include(t => t.TenderOffers)
                    .FirstOrDefault(t => t.CreatedTime == newTenderOffer.TenderCreatedDate);
                if (tender == null)
                {
                    iteration.Item1.BasicAck(ea.DeliveryTag, false);
                    return;
                }

                tender.AddTenderOffer(tenderOffer);
                _uow.GetRepository<ITenderWriteRepository>().Update(tender);
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

        public override void Dispose()
        {
            foreach (var channel in _channels)
            {
                channel.Close();
            }
            _connection.Close();
            _runThread = false;
            base.Dispose();
        }
    }
}
