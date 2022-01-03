using Integration.Shared.Model;
using Integration.Tendering.Repository;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;
using IntegrationAPI.DTO.Tender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Microsoft.EntityFrameworkCore;
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
                    foreach (var pharmacyApiKey in pharmacies.Select(x => x.ApiKey))
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.ExchangeDeclare("new tender", ExchangeType.Direct);
                            TenderToPharmacyDto dto = new TenderToPharmacyDto
                            {
                                Name = tender.Name,
                                Apikey = pharmacyApiKey,
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
                            channel.BasicPublish("new tender", pharmacyApiKey.ToString(), null, body);
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

        [HttpGet]
        public IEnumerable<Tender> GetActiveTenders()
        {
            List<Tender> retVal = new List<Tender>();
            var tenders = _uow.GetRepository<ITenderReadRepository>().GetAll()
                .Include(t => t.TenderOffers).Include(t => t.MedicationRequests);
            foreach (var tender in tenders.AsEnumerable().Where(t => t.IsActive() == true))
            {
                retVal.Add(tender);
            }

            return retVal;
        }

        [HttpPost]
        public IActionResult ChooseWinningOffer(WinningOfferDto dto)
        {
            var tender = _uow.GetRepository<ITenderReadRepository>().GetAll().Where(t => t.Id == dto.TenderId)
                .Include(t => t.TenderOffers).ThenInclude(to => to.Pharmacy).FirstOrDefault();
            var winningOffer = tender.TenderOffers.FirstOrDefault(t => t.Id == dto.TenderOfferId);
            tender.ChooseWinner(winningOffer);
            tender.CloseTender();
            _uow.GetRepository<ITenderWriteRepository>().Update(tender);
            try
            {
                var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("declare winning offer", ExchangeType.Direct);
                    var msg = new WinningOfferToPharmacyDto
                    {
                        ApiKey = tender.WinningOffer.Pharmacy.ApiKey,
                        TenderClosedDate = tender.ClosedTime,
                        TenderCreatedDate = tender.CreatedTime,
                        TenderOfferCreatedDate = tender.WinningOffer.CreatedDate
                    };
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
                    channel.BasicPublish("declare winning offer", tender.WinningOffer.Pharmacy.ApiKey.ToString(), null, body);
                }

                var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll().Where(p => p.Id != tender.WinningOffer.PharmacyId);
                CloseTenderRmq(factory, pharmacies, tender);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while sending closed tender via rabbitmq!");
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult CloseTender(int tenderId)
        {
            var tender = _uow.GetRepository<ITenderReadRepository>().GetById(tenderId);
            if (tender == null) return NotFound("Tender does not exist");
            tender.CloseTender();
            _uow.GetRepository<ITenderWriteRepository>().Update(tender);
            try
            {
                var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll();
                var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };
                CloseTenderRmq(factory, pharmacies, tender);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while sending closed tender via rabbitmq!");
            }
            return Ok();
        }

        private static void CloseTenderRmq(ConnectionFactory factory, IEnumerable<Pharmacy> pharmacies, Tender tender)
        {
            using (var connection = factory.CreateConnection())
            {
                foreach (var pharmacyApiKey in pharmacies.Select(x => x.ApiKey))
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare("close tender", ExchangeType.Direct);
                        var msg = new CloseTenderToPharmaciesDto
                        {
                            ApiKey = pharmacyApiKey,
                            TenderClosedDate = tender.ClosedTime,
                            TenderCreatedDate = tender.CreatedTime
                        };
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
                        channel.BasicPublish("close tender", pharmacyApiKey.ToString(), null, body);
                    }
                }
            }
        }
    }
}
