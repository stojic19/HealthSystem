using System;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : BasePharmacyController
    {

        public RegistrationController(IUnitOfWork uow, PharmacyDetails pharmacyDetails) : base(uow, pharmacyDetails)
        {
        }

        [HttpPost]
        public IActionResult RegisterHospital(RegisterHospitalDTO newHospital)
        {
            var cityReadRepo = UoW.GetRepository<ICityReadRepository>();
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
                StreetNumber = newHospital.StreetNumber,
                BaseUrl = newHospital.BaseUrl
            };

            var str = Url.Action("Add", "Medicine", Request.Scheme);

            try
            {
                UoW.GetRepository<IHospitalWriteRepository>().Add(hospital);
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
                PharmacyName = PharmacyDetails.Name,
                CityName = PharmacyDetails.CityName,
                StreetName = PharmacyDetails.StreetName,
                StreetNumber = PharmacyDetails.StreetNumber,
                CountryName = PharmacyDetails.CountryName,
                PostalCode = PharmacyDetails.PostalCode
            };
        }
    }
}
