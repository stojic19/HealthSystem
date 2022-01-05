using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Exceptions;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using Pharmacy.Services;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
using PharmacyApi.HttpRequestSenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TenderingController : BasePharmacyController
    {
        private readonly TenderOffersService _tenderOffersService;
        private readonly IUnitOfWork _uow;
        private readonly IHttpRequestSender _httpRequestSender;

        public TenderingController(IUnitOfWork uow, PharmacyDetails details, IHttpRequestSender requestSender) : base(uow, details)
        {
            _tenderOffersService = new TenderOffersService(uow);
            _uow = uow;
            
            _httpRequestSender = requestSender;
        }

        [HttpPost]
        public IActionResult Create(TenderOfferDTO tenderOfferDTO)
        {
            if (!IsApiKeyValid(tenderOfferDTO.ApiKey))
                return BadRequest(ModelState);
            Money cost = new Money(tenderOfferDTO.MoneyDto.Amount, tenderOfferDTO.MoneyDto.Currency);
            List<MedicationRequest> medicationRequests = new List<MedicationRequest>();
            try
            {
                foreach (MedicationRequestDTO dto in tenderOfferDTO.MedicationRequestDtos)
                {
                    medicationRequests.Add(new MedicationRequest(dto.MedicineName, dto.Quantity));
                }
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);

            }
            try
            {
                _tenderOffersService.CreateTenderOffer(tenderOfferDTO.ApiKey, medicationRequests, cost, tenderOfferDTO.TenderId);
            }
            catch (MedicineUnavailableException exception) { return NotFound(exception.Message); }
            catch (MedicineFromManufacturerNotFoundException exception) { return NotFound(exception.Message); }
            catch (RabbitMQNewOfferException exception) { return StatusCode(StatusCodes.Status500InternalServerError, exception.Message); }

            return Ok("Tender offer succesfully created!");
        }

        [HttpPost]
        public IActionResult Confirm(Guid apiKey, int tenderOfferId)
        {
            if (!IsApiKeyValid(apiKey))
                return BadRequest(ModelState);

            try
            {
                _tenderOffersService.ConfirmTenderOffer(apiKey, tenderOfferId);
            }
            catch (TenderNotFoundException exception) { return NotFound(exception.Message); }
            catch (MedicineUnavailableException exception) { return NotFound(exception.Message); }
            catch (UnauthorizedAccessException exception) { return Unauthorized(exception.Message); }
            catch (TenderAlreadyEnabledException exception) { return BadRequest(exception.Message); }

            TenderOffer tenderOffer = _uow.GetRepository<ITenderOfferReadRepository>().GetAll().Include(x => x.Tender).ThenInclude(x => x.Hospital).FirstOrDefault(tender => tender.Id == tenderOfferId);
            TenderProcurementDTO tenderProcurementDTO = CreateTenderProcurementDTO(tenderOffer);
            var response = _httpRequestSender.Post(tenderOffer.Tender.Hospital.BaseUrl + "/api/Tender/ExecuteTenderProcurement", tenderProcurementDTO);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return BadRequest("Unable to reach integration API!");
            return Ok("Tender offer succesfully confirmed!");
        }

        private TenderProcurementDTO CreateTenderProcurementDTO(TenderOffer tenderOffer)
        {
            List<TenderMedicineDTO> tenderMedicineDtos = new List<TenderMedicineDTO>();
            foreach (MedicationRequest medReq in tenderOffer.MedicationRequests)
            {
                Medicine medicine = _uow.GetRepository<IMedicineReadRepository>().GetMedicineByName(medReq.MedicineName);
                List<string> substances = new List<string>();
                foreach (var substance in medicine.Substances)
                {
                    substances.Add(substance.Name);
                }

                tenderMedicineDtos.Add(
                    new TenderMedicineDTO
                    {
                        Medicine = new MedicineDTO
                        {
                            Name = medicine.Name,
                            Manufacturer = medicine.Manufacturer.Name,
                            SideEffects = medicine.SideEffects,
                            Reactions = medicine.Reactions,
                            Precautions = medicine.Precautions,
                            MedicinePotentialDangers = medicine.MedicinePotentialDangers,
                            Substances = substances,
                            Type = medicine.Type,
                            Usage = medicine.Usage,
                            WeightInMilligrams = medicine.WeightInMilligrams
                        },
                        Quantity = medReq.Quantity
                    });
            }

            TenderProcurementDTO tenderProcurementDto = new TenderProcurementDTO
            {
                ApiKey = tenderOffer.Tender.Hospital.ApiKey,
                Medications = tenderMedicineDtos
            };
            return tenderProcurementDto;
        }

        private ApplyTenderOfferDTO CreateApplyTenderDto(int tenderOfferId,double price)
        {
            TenderOffer tenderOffer = _uow.GetRepository<ITenderOfferReadRepository>().GetAll().Include(x => x.Tender).FirstOrDefault(tender => tender.Id == tenderOfferId);
            List<MedicationRequestDTO> medicationRequestDtos = new List<MedicationRequestDTO>();
            foreach (MedicationRequest medReq in tenderOffer.MedicationRequests)
            {
                medicationRequestDtos.Add(new MedicationRequestDTO{MedicineName = medReq.MedicineName, Quantity = medReq.Quantity});
            }

            ApplyTenderOfferDTO applyTenderOfferDTO = new ApplyTenderOfferDTO() {
                TenderOfferId = tenderOfferId,
                MedicationRequestDto = medicationRequestDtos,
                CreationTime = tenderOffer.CreationTime,
                TotalPrice = price,
                ApiKey = tenderOffer.Tender.Hospital.ApiKey
            };
            return applyTenderOfferDTO;
        }

        [HttpPost]
        public IActionResult CreateTenderTest()
        {
            Tender tender = new Tender()
            {
                ActiveRange = new TimeRange(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(3)),
                ClosedDate = DateTime.MinValue,
                HospitalId = 34
            };
            UoW.GetRepository<ITenderWriteRepository>().Add(tender);
            TenderOffer tenderOffer = new TenderOffer()
            {
                TenderId = tender.Id,
                Cost = new Money(20, 0),
                IsConfirmed = false,
                CreationTime = DateTime.Now
            };
            UoW.GetRepository<ITenderOfferWriteRepository>().Add(tenderOffer);
            return Ok(tenderOffer);
        }


    }
}
