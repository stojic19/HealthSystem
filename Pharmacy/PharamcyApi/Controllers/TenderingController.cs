using Microsoft.AspNetCore.Mvc;
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
using System.Threading.Tasks;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TenderingController : BasePharmacyController
    {
        private readonly TenderOffersService _tenderOffersService;
        private readonly IUnitOfWork _uow;
        private readonly string _integrationEndPoint;
        private readonly IHttpRequestSender _httpRequestSender;

        public TenderingController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {
            _tenderOffersService = new TenderOffersService(uow);
            _uow = uow;
            //FOR INTEGRATION TO SET ON NEXT SPRINT!
            _integrationEndPoint = "";
        }

        [HttpPost]
        public IActionResult Create(TenderOfferDTO tenderOfferDTO)
        {
            if (!IsApiKeyValid(tenderOfferDTO.ApiKey))
                return BadRequest(ModelState);

            try
            {
                _tenderOffersService.CreateTenderOffer(tenderOfferDTO.ApiKey, tenderOfferDTO.MedicineName, tenderOfferDTO.Quantity, tenderOfferDTO.CreationTime);
            }
            catch (MedicineUnavailableException exception) { return NotFound(exception.Message); }
            catch (MedicineFromManufacturerNotFoundException exception) { return NotFound(exception.Message); }

            return Ok("Tender offer succesfully created!");
        }

        [HttpPost]
        public IActionResult ApplyForTender(int tenderOfferId)
        {
            double price;
            try
            {
                price = _tenderOffersService.GetTenderPrice(tenderOfferId);
            }
            catch (TenderNotFoundException exception) { return NotFound(exception.Message); }
            catch (MedicineUnavailableException exception) { return NotFound(exception.Message); }
            catch (TenderAlreadyEnabledException exception) { return BadRequest(exception.Message); }

            ApplyTenderOfferDTO applyTenderOfferDTO = CreateApplyTenderDto(tenderOfferId, price);
            var response = _httpRequestSender.Post(_integrationEndPoint, applyTenderOfferDTO);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return BadRequest("Unable to reach integration API!");

            return Ok("Succesfully applied for tender!");
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
            catch( UnauthorizedAccessException exception) { return Unauthorized(exception.Message); }
            catch( TenderAlreadyEnabledException exception) { return BadRequest(exception.Message); }

            return Ok("Tender offer succesfully confirmed!");
        }

        private ApplyTenderOfferDTO CreateApplyTenderDto(int tenderOfferId,double price)
        {
            TenderOffer tenderOffer = _uow.GetRepository<ITenderOfferReadRepository>().GetById(tenderOfferId);
            
            ApplyTenderOfferDTO applyTenderOfferDTO = new ApplyTenderOfferDTO() {
                TenderOfferId = tenderOfferId,
                MedicineName = tenderOffer.Medicine.Name,
                Quantity = tenderOffer.Quantity,
                CreationTime = DateTime.Now,
                TotalPrice = price,
                ApiKey = tenderOffer.Hospital.ApiKey
            };
            return applyTenderOfferDTO;
        }



    }
}
