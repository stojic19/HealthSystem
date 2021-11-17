using System;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly PharmacyDetails _pharmacyDetails;

        public RegistrationController(IUnitOfWork uow, PharmacyDetails pharmacyDetails)
        {
            _uow = uow;
            _pharmacyDetails = pharmacyDetails;
        }

        [HttpPost]
        public IActionResult RegisterHospital(RegisterHospitalDTO newHospital)
        {
            var cityReadRepo = _uow.GetRepository<ICityReadRepository>();
            if (!cityReadRepo.CheckIfExists(newHospital.CityName))
            {
                return BadRequest(Responses.CityNotFound);
            }

            var hospital = new Hospital()
            {
                ApiKey = Guid.NewGuid(),
                CityId = cityReadRepo.GetCityByName(newHospital.CityName).Id,
                Name = newHospital.Name,
                StreetName = newHospital.StreetName,
                StreetNumber = newHospital.StreetNumber
            };

            var str = Url.Action("Add", "Medicine", Request.Scheme);

            try
            {
                _uow.GetRepository<IHospitalWriteRepository>().Add(hospital);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            var response = CreateResponse(hospital);

            return Ok(response);
        }

        private HospitalRegisteredDTO CreateResponse(Hospital hospital)
        {
            return new HospitalRegisteredDTO()
            {
                ApiKey = hospital.ApiKey,
                BaseUrl = $"{Request.Scheme}://{Request.Host}",
                PharmacyName = _pharmacyDetails.Name,
                CityName = _pharmacyDetails.CityName,
                StreetName = _pharmacyDetails.StreetName,
                StreetNumber = _pharmacyDetails.StreetNumber,
                CountryName = _pharmacyDetails.CountryName
            };
        }
    }
}
