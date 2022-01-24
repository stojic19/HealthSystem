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
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Service;
using IntegrationAPI.Controllers.Base;
using IntegrationApi.DTO.Shared;
using IntegrationAPI.DTO.Shared;
using IntegrationApi.DTO.Tender;
using IntegrationAPI.HttpRequestSenders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using IntegrationAPI.Adapters.PDF;
using IntegrationAPI.Adapters.PDF.Implementation;
using System.IO;
using System.Threading;
using IntegrationApi.Messages;

namespace IntegrationAPI.Controllers.Tenders
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TenderController : BaseIntegrationController
    {

        public TenderController(IUnitOfWork uow, IHttpRequestSender requestSender) : base(uow, requestSender) { }

        [HttpPost, Produces("application/json")]
        public IActionResult CreateTender(CreateTenderDto createTenderDto)
        {
            if (createTenderDto.Name.Equals("")) return BadRequest(TenderMessages.InvalidTenderName);
            if (DateTime.Compare(createTenderDto.EndDate, createTenderDto.StartDate) < 0) return BadRequest(TenderMessages.InvalidDateRange);
            if (createTenderDto.MedicineRequests.Count == 0) return BadRequest(TenderMessages.InvalidMedicineList);
            foreach (MedicineRequestDto medicineRequestDto in createTenderDto.MedicineRequests)
            {
                if (medicineRequestDto.MedicineName.Equals("")) return BadRequest(TenderMessages.InvalidMedicineName);
                if (medicineRequestDto.Quantity <= 0) return BadRequest(TenderMessages.InvalidQuantity(medicineRequestDto.MedicineName));
            }

            Tender tender;
            try
            {
                tender = SaveTender(createTenderDto);
            }
            catch(ArgumentException e)
            {
                return Problem(e.Message);
            }
            var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().ToList();

            try
            {
                SendTender(pharmacies, tender);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, TenderMessages.FailedToSendTender);
            }
            return Ok(TenderMessages.Created);
        }

        private void SendTender(IEnumerable<Pharmacy> pharmacies, Tender tender)
        {
            var factory = new ConnectionFactory() {HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST")};
            using (var connection = factory.CreateConnection())
            {
                foreach (var pharmacyApiKey in pharmacies.Select(x => x.ApiKey))
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare("new tender", ExchangeType.Direct);
                        var dto = CreateTenderToPharmacyDto(tender, pharmacyApiKey);

                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
                        channel.BasicPublish("new tender", pharmacyApiKey.ToString(), null, body);
                    }

                    if (pharmacies.Any()) new EmailService().SendNewTenderMail(tender, pharmacies);
                }
            }
        }

        private TenderToPharmacyDto CreateTenderToPharmacyDto(Tender tender, Guid pharmacyApiKey)
        {
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

            return dto;
        }

        private Tender SaveTender(CreateTenderDto createTenderDto)
        {
            Tender tender = new Tender(createTenderDto.Name, new TimeRange(createTenderDto.StartDate, createTenderDto.EndDate));
            foreach (MedicineRequestDto medicineRequestDto in createTenderDto.MedicineRequests)
                tender.AddMedicationRequest(new MedicationRequest(medicineRequestDto.MedicineName,
                    medicineRequestDto.Quantity));
            _unitOfWork.GetRepository<ITenderWriteRepository>().Add(tender);
            return tender;
        }

        [HttpGet]
        public IEnumerable<Tender> GetActiveTenders()
        {
            List<Tender> retVal = new List<Tender>();
            var tenders = _unitOfWork.GetRepository<ITenderReadRepository>().GetAll()
                .Include(t => t.TenderOffers).Include(t => t.MedicationRequests);
            foreach (var tender in tenders.AsEnumerable().Where(t => t.IsActive())) retVal.Add(tender);
            return retVal;
        }

        [HttpGet("{id:int}")]
        public Tender GetTenderById(int id)
        {
            var tenders = _unitOfWork.GetRepository<ITenderReadRepository>().GetAll()
                            .Include(t => t.TenderOffers).ThenInclude(to => to.Pharmacy)
                            .Include(t => t.MedicationRequests);
            foreach (var tender in tenders.AsEnumerable().Where(t => t.IsActive()))
                if (tender.Id.Equals(id))
                    return tender;
            return null;
        }

        [HttpGet("{id:int}")]
        public PharmacyStatsDTO GetTenderStatsForPharmacy(int id)
        {
            PharmacyStatsDTO stats = new PharmacyStatsDTO();
            Pharmacy pharmacy = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll()
                .Include(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.Complaints)
                .FirstOrDefault(x => x.Id == id);

            var tenders = _unitOfWork.GetRepository<ITenderReadRepository>().GetAll()
                            .Include(t => t.TenderOffers).ThenInclude(to => to.Pharmacy).Include(t => t.MedicationRequests);
            foreach (var tender in tenders.AsEnumerable())
            {
                stats.Offers += tender.NumberOfPharmacyOffers(pharmacy);
                if (tender.DidPharmacyWin(pharmacy))
                    stats.Won += 1;     
            }
            return stats;
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
                SendWinningTender(tender);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, TenderMessages.FailedToSendClosedTender);
            }
            return Ok(TenderMessages.Winner);
        }

        private void SendWinningTender(Tender tender)
        {
            var factory = new ConnectionFactory() {HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST")};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("declare winning offer", ExchangeType.Direct);
                var winningOfferToPharmacyDto = CreateWinningOfferToPharmacyDto(tender);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(winningOfferToPharmacyDto));
                channel.BasicPublish("declare winning offer", tender.WinningOffer.Pharmacy.ApiKey.ToString(), null, body);
            }

            var mailServ = new EmailService();
            mailServ.SendWinningOfferMail(tender);

            var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll()
                .Where(p => p.Id != tender.WinningOffer.PharmacyId);
            CloseTenderRmq(factory, pharmacies, tender);
            if (pharmacies.Any()) mailServ.SendCloseTenderMail(tender, pharmacies);
        }

        private static WinningOfferToPharmacyDto CreateWinningOfferToPharmacyDto(Tender tender)
        {
            var winningOfferToPharmacyDto = new WinningOfferToPharmacyDto
            {
                ApiKey = tender.WinningOffer.Pharmacy.ApiKey,
                TenderClosedDate = tender.ClosedTime,
                TenderCreatedDate = tender.CreatedTime,
                TenderOfferCreatedDate = tender.WinningOffer.CreatedDate
            };
            return winningOfferToPharmacyDto;
        }

        [HttpPost]
        public IActionResult CloseTender([FromBody]int tenderId)
        {
            var tender = _unitOfWork.GetRepository<ITenderReadRepository>().GetById(tenderId);
            if (tender == null) return NotFound(TenderMessages.NotFound);
            if (!tender.IsActive()) return BadRequest(TenderMessages.AlreadyClosed);
            tender.CloseTender();
            _unitOfWork.GetRepository<ITenderWriteRepository>().Update(tender);
            try
            {
                var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
                var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") };
                CloseTenderRmq(factory, pharmacies, tender);
                if (pharmacies.Any()) new EmailService().SendCloseTenderMail(tender, pharmacies);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, TenderMessages.FailedToSendClosedTender);
            }
            return Ok(TenderMessages.Closed);
        }

        [HttpPost]
        public IActionResult ExecuteTenderProcurement(TenderProcurementDto tenderProcurementDto)
        {
            var pharmacy = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().FirstOrDefault(p => p.ApiKey == tenderProcurementDto.ApiKey);
            if (pharmacy == null) return NotFound(PharmacyMessages.NotRegistered);

            string targetUrl = _hospitalBaseUrl + "/api/Medication/AddMedicineTender";
            IRestResponse response = _httpRequestSender.Post(targetUrl, tenderProcurementDto);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest(MedicineMessages.CouldNotAddToHospital);
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
                        var closeTenderToPharmaciesDto = CreateCloseTenderToPharmaciesDto(tender, pharmacyApiKey);
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(closeTenderToPharmaciesDto));
                        channel.BasicPublish("close tender", pharmacyApiKey.ToString(), null, body);
                    }
                }
            }
        }

        private static CloseTenderToPharmaciesDto CreateCloseTenderToPharmaciesDto(Tender tender, Guid pharmacyApiKey)
        {
            var closeTenderToPharmaciesDto = new CloseTenderToPharmaciesDto
            {
                ApiKey = pharmacyApiKey,
                TenderClosedDate = tender.ClosedTime,
                TenderCreatedDate = tender.CreatedTime
            };
            return closeTenderToPharmaciesDto;
        }

        [HttpPost]
        public TenderStatisticsDto GetTenderStatistics(TimePeriodDTO timePeriodDto)
        {
            TimeRange timeRange = new TimeRange(timePeriodDto.StartTime, timePeriodDto.EndTime);
            TenderStatisticsDto tenderStatisticsDto = new TenderStatisticsDto()
            {
                PharmacyStatistics = new List<PharmacyTenderStatisticsDto>()
            };
            var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            var tenders = _unitOfWork.GetRepository<ITenderReadRepository>()
                .GetAll()
                .Include(x => x.TenderOffers)
                .ThenInclude(x => x.Pharmacy)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.MedicationRequests)
                .ToList();

            foreach (var pharmacy in pharmacies)
            {
                CreatePharmacyStatistic(tenders, timeRange, pharmacy, tenderStatisticsDto);
            }
            IPDFAdapter adapter = new DynamicPDFAdapter();
            tenderStatisticsDto.PdfUrl = adapter.MakeTenderStatisticsPdf(tenderStatisticsDto, timeRange);
            return tenderStatisticsDto;
        }

        private void CreatePharmacyStatistic(List<Tender> tenders, TimeRange timeRange, Pharmacy pharmacy,
            TenderStatisticsDto tenderStatisticsDto)
        {
            double profitAmount = 0;
            int tendersEntered = 0;
            int tenderOffersMade = 0;
            int tendersWon = 0;
            foreach (var tender in tenders)
            {
                if (!tender.ActiveRange.OverlapsWith(timeRange))
                    continue;
                int pharmacyOffers = tender.NumberOfPharmacyOffers(pharmacy);
                if (pharmacyOffers > 0)
                {
                    tendersEntered++;
                    tenderOffersMade += pharmacyOffers;
                }

                if (tender.DidPharmacyWin(pharmacy))
                {
                    tendersWon++;
                    profitAmount += tender.WinningOffer.Cost.Amount;
                }
            }

            tenderStatisticsDto.PharmacyStatistics.Add(new PharmacyTenderStatisticsDto()
            {
                PharmacyId = pharmacy.Id,
                PharmacyName = pharmacy.Name,
                Profit = new MoneyDto()
                {
                    Amount = profitAmount,
                    Currency = 0
                },
                TendersEntered = tendersEntered,
                TendersWon = tendersWon,
                TenderOffersMade = tenderOffersMade
            });
        }

        [HttpPost, Produces("application/pdf")]
        public IActionResult GetStatisticsPdf([FromQuery(Name = "fileName")] string fileName)
        {
            string destDirectory = "TenderStatistics";

            string destFileName = Path.GetFullPath(System.IO.Path.Combine(destDirectory, fileName));
            string fullDestDirPath = Path.GetFullPath(destDirectory + Path.DirectorySeparatorChar);
            if (destFileName.StartsWith(fullDestDirPath, StringComparison.Ordinal))
            {
                try
                {
                    var stream = new FileStream(destFileName, FileMode.Open);
                    return File(stream, "application/pdf", fileName);
                }
                catch
                {
                    return NotFound(PdfMessages.NotFound);
                }
            }
            return BadRequest(PdfMessages.CannotOpen);
        }
    }
}
