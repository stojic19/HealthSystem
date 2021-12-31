using Integration.Shared.Model;
using Integration.Tendering.Repository;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;
using IntegrationAPI.DTO.Tender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IntegrationAPI.Controllers.Tenders
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public TenderController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost, Produces("application/json")]
        public IActionResult CreateTender(CreateTenderDto createTenderDto)
        {
            if (createTenderDto.Name.Equals(""))
            {
                return BadRequest("Invalid tender name.");
            }
            if(DateTime.Compare(createTenderDto.EndDate, createTenderDto.StartDate)<0)
            {
                return BadRequest("Start date must be before end date.");
            }
            if (createTenderDto.MedicineRequests.Count == 0)
            {
                return BadRequest("No medicine requests in list.");
            }
            foreach (MedicineRequestDto medicineRequestDto in createTenderDto.MedicineRequests)
            {
                if (medicineRequestDto.MedicineName.Equals(""))
                {
                    return BadRequest("Medicine name required.");
                }
                if (medicineRequestDto.Quantity <= 0)
                {
                    return BadRequest("Invalid quantity for " + medicineRequestDto.MedicineName + ".");
                }
            }
            Tender tender = new Tender(createTenderDto.Name, new TimeRange(createTenderDto.StartDate, createTenderDto.EndDate));
            foreach(MedicineRequestDto medicineRequestDto in createTenderDto.MedicineRequests)
            {
                tender.AddMedicationRequest(new MedicationRequest(medicineRequestDto.MedicineName, medicineRequestDto.Quantity));
            }
            _uow.GetRepository<ITenderWriteRepository>().Add(tender);
            var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll();
            try
            {
                var factory = new ConnectionFactory() {HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST")};
                using (var connection = factory.CreateConnection())
                {
                    foreach (var pharmacy in pharmacies)
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.ExchangeDeclare("new tender", ExchangeType.Direct);
                            TenderToPharmacyDto dto = new TenderToPharmacyDto
                            {
                                Name = tender.Name,
                                Apikey = pharmacy.ApiKey,
                                CreatedDate = tender.CreatedTime,
                                EndDate = tender.ActiveRange.EndDate,
                                StartDate = tender.ActiveRange.StartDate,
                                MedicationRequestDto = new List<MedicationRequestDto>()
                            };
                            foreach (MedicationRequest req in tender.MedicationRequests)
                            {
                                dto.MedicationRequestDto.Add(new MedicationRequestDto
                                {
                                    MedicineName = req.MedicineName,
                                    Quantity = req.Quantity
                                });
                            }

                            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
                            channel.BasicPublish("new tender", pharmacy.ApiKey.ToString(), null, body);
                        }
                    }
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while sending tender via rabbitmq!");
            }
            return Ok("Tender created");
        }
    }
}
