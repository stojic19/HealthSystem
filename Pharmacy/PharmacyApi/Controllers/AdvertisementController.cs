using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Exceptions;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using Pharmacy.Services;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
using PharmacyApi.DTO.Base;

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

        [HttpPut]
        public IActionResult CreateAdvertisement(CreateAdvertisementDTO adDTO)
        {
            try
            {
                _advertisementService.CreateAdvertisement(adDTO.Title,adDTO.Description,adDTO.MedicineId);
            }
            catch(MedicineNotFoundException exception){return NotFound(exception.Message);}
          
            return Ok("Ad successfully created!");
        }

        [HttpGet]
        public IActionResult GetAdvertisements(Guid apiKey)
        {
            if (!IsApiKeyValid(apiKey)) return BadRequest("Api key not found!");
            var advertisements = UoW.GetRepository<IAdvertisementReadRepository>().GetAll()
                .Include(a => a.Medicine)
                .ToList();
            List<AdvertisementDto> retVal = new List<AdvertisementDto>();
            foreach(var ad in advertisements)
                retVal.Add(new AdvertisementDto()
                {
                    Description = ad.Description,
                    MedicationName = ad.Medicine.Name,
                    Title = ad.Title
                });
            return Ok(retVal);
        }

        [HttpPost]
        public IActionResult UpdateAdvertisement(UpdateAdvertisementDTO adDTO)
        {
            try
            {
                _advertisementService.UpdateAdvertisement(adDTO.AdvertisementId,adDTO.Title,adDTO.Description);
            }
            catch (AdvertisementNotFoundException exception) { return NotFound(exception.Message); }

            return Ok("Ad successfully updated!");
        }

        [HttpDelete]
        public IActionResult DeleteAdvertisement([FromBody] int id)
        {
            try
            {
                _advertisementService.DeleteAdvertisement(id);
            }
            catch (AdvertisementNotFoundException exception) { return NotFound(exception.Message); }

            return Ok("Ad successfully deleted!");
        }
    }
}
