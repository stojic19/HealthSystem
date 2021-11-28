﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Threading;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using IntegrationAPI.DTO;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace Integration.MicroServices
{
    
    public class BenefitRabbitMqService : BackgroundService
    {
        IConnection _connection;
        IModel _channel;
        private IUnitOfWork _uow;
        private string queueName;

        public BenefitRabbitMqService(IUnitOfWork uow)
        {
            _uow = uow;
            InitRabbitMQ();
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("benefits", ExchangeType.Fanout);
            queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName,
                exchange: "benefits",
                routingKey: "");
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                Console.WriteLine("Message received: " + jsonMessage);
                BenefitCreateDTO dto = JsonConvert.DeserializeObject<BenefitCreateDTO>(jsonMessage);
                Pharmacy pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetByName(dto.PharmacyName).ElementAt(0);
                Benefit benefit = new Benefit()
                {
                    Description = dto.Description,
                    EndTime = dto.EndTime,
                    Hidden = false,
                    PharmacyId = pharmacy.Id,
                    Published = false,
                    StartTime = dto.StartTime,
                    Title = dto.Title
                };
                _uow.GetRepository<IBenefitWriteRepository>().Add(benefit);
                Console.WriteLine(jsonMessage);
                Console.WriteLine(benefit.Description);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;


            _channel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
