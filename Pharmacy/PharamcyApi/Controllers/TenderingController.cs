using Microsoft.AspNetCore.Mvc;
using Pharmacy.Exceptions;
using Pharmacy.Repositories.Base;
using Pharmacy.Services;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
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
        public TenderingController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {
            _tenderOffersService = new TenderOffersService(uow);
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
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (MedicineUnavailableException exception)
            {
                return NotFound(exception.Message);
            }

            return Ok("Tender offer succesfully created!");
        }



    }
}
