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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using IntegrationAPI.Controllers.Base;
using IntegrationAPI.HttpRequestSenders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;

namespace IntegrationAPI.Controllers.Tenders
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TenderController : BaseIntegrationController
    {
        private readonly IHttpRequestSender _requestSender;

        public TenderController(IUnitOfWork uow, IHttpRequestSender requestSender) : base(uow)
        {
            _requestSender = requestSender;
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
            _unitOfWork.GetRepository<ITenderWriteRepository>().Add(tender);
            var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll();
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
            var tenders = _unitOfWork.GetRepository<ITenderReadRepository>().GetAll()
                .Include(t => t.TenderOffers).Include(t => t.MedicationRequests);
            foreach (var tender in tenders.AsEnumerable().Where(t => t.IsActive()))
            {
                retVal.Add(tender);
            }

            return retVal;
        }

        [HttpPost]
        public IActionResult ChooseWinningOffer(WinningOfferDto dto)
        {
            var tender = _unitOfWork.GetRepository<ITenderReadRepository>().GetAll().Where(t => t.Id == dto.TenderId)
                .Include(t => t.TenderOffers).ThenInclude(to => to.Pharmacy).FirstOrDefault(); 
            if (tender == null) return NotFound("Tender does not exist");
            if (!tender.IsActive()) return BadRequest("Tender already closed!");
            var winningOffer = tender.TenderOffers.FirstOrDefault(t => t.Id == dto.TenderOfferId);
            if(winningOffer == null) return BadRequest("Invalid tender offer!");
            tender.ChooseWinner(winningOffer);
            tender.CloseTender();
            _unitOfWork.GetRepository<ITenderWriteRepository>().Update(tender);
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

                var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().Where(p => p.Id != tender.WinningOffer.PharmacyId);
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
            var tender = _unitOfWork.GetRepository<ITenderReadRepository>().GetById(tenderId);
            if (tender == null) return NotFound("Tender does not exist");
            if (!tender.IsActive()) return BadRequest("Tender already closed!");
            tender.CloseTender();
            _unitOfWork.GetRepository<ITenderWriteRepository>().Update(tender);
            try
            {
                var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll();
                var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };
                CloseTenderRmq(factory, pharmacies, tender);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while sending closed tender via rabbitmq!");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ExecuteTenderProcurement(TenderProcurementDto tenderProcurementDto)
        {
            var pharmacy = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().FirstOrDefault(p => p.ApiKey == tenderProcurementDto.ApiKey);
            if (pharmacy == null) return NotFound("Pharmacy not registered in hospital");

            string targetUrl = _hospitalBaseUrl + "/api/Medication/AddMedicineTender";
            IRestResponse response = _requestSender.Post(targetUrl, tenderProcurementDto);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest("Hospital could not add received medicine");
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
