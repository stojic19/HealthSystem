using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PharmacyApi.DTO;
using RabbitMQ.Client;
using Pharmacy.Model;
using Pharmacy.Repositories;
using PharmacyApi.Controllers.Base;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BenefitController : BasePharmacyController
    {
        public BenefitController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {
        }


        [HttpPost]
        public void SendBenefit(BenefitIdDTO dto)
        {
            Benefit benefit = null;
            try
            {
                benefit = UoW.GetRepository<IBenefitReadRepository>()
                    .GetById(dto.BenefitId);

                var factory = new ConnectionFactory() {HostName = "localhost"};
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "BenefitCommunication",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    BenefitSendDTO message = new BenefitSendDTO()
                    {
                        Description = benefit.Description,
                        EndTime = benefit.EndTime,
                        StartTime = benefit.StartTime,
                        Title = benefit.Title
                    };
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(exchange: "",
                        routingKey: "BenefitCommunication",
                        basicProperties: null,
                        body: body);
                }
            }
            catch (Exception e)
            {
                return;
            }

        }

        [HttpPost]
        public IActionResult AddBenefit(BenefitMakeDTO dto)
        {
            var benefit = new Benefit()
            {
                Title = dto.Title,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            try
            {
                UoW.GetRepository<IBenefitWriteRepository>()
                    .Add(benefit);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return Ok(Responses.Success);
        }
    }
}
