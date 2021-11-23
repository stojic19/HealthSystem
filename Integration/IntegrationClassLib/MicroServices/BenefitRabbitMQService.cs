using System;
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
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace Integration.MicroServices
{
    class BenefitRabbitMQService : BackgroundService
    {
        IConnection connection;
        IModel channel;
        private IUnitOfWork _uow;

        public BenefitRabbitMQService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "BenefitCommunication",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                Benefit benefit;
                try
                {
                    benefit = JsonConvert.DeserializeObject<Benefit>(jsonMessage);
                    benefit.Hidden = false;
                    benefit.Published = false;
                    _uow.GetRepository<IBenefitWriteRepository>().Add(benefit);
                }
                catch (Exception)
                {
                    Console.Write("Exception caught");
                    return;
                }
            };
            channel.BasicConsume(queue: "BenefitCommunication",
                                    autoAck: true,
                                    consumer: consumer);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Close();
            connection.Close();
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
