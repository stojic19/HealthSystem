using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Exceptions;
using Pharmacy.Repositories.Base;
using Pharmacy.Services;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdvertisementController : BasePharmacyController
    {
        private readonly AdvertisementService _advertisementService;
        public AdvertisementController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {
            _advertisementService = new AdvertisementService(uow);
        }

        [HttpPost]
        public IActionResult CreateAdvertisement(CreateAdvertisementDTO adDTO)
        {
            if (!IsApiKeyValid(adDTO.ApiKey))
                return BadRequest(ModelState);

            try
            {
                _advertisementService.CreateAdvertisement(adDTO.Title,adDTO.Description,adDTO.MedicineId);
            }
            catch(MedicineNotFoundException exception){return NotFound(exception.Message);}
          
            return Ok("Ad successfully created!");
        }

        [HttpDelete]
        public IActionResult DeleteAdvertisement(Guid apiKey,int id)
        {
            if (!IsApiKeyValid(apiKey))
                return BadRequest(ModelState);

            try
            {
                _advertisementService.DeleteAdvertisement(id);
            }
            catch(AdvertisementNotFoundException exception) { return NotFound(exception.Message); }

            return Ok("Ad successfully deleted!");
        }

        [HttpPut]
        public IActionResult UpdateAdvertisement(UpdateAdvertisementDTO adDTO)
        {
            if (!IsApiKeyValid(adDTO.ApiKey))
                return BadRequest(ModelState);

            try
            {
                _advertisementService.UpdateAdvertisement(adDTO.AdvertisementId,adDTO.Title,adDTO.Description);
            }
            catch (AdvertisementNotFoundException exception) { return NotFound(exception.Message); }

            return Ok("Ad successfully updated!");
        }
    }
}
