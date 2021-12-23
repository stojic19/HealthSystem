using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO.Pharmacies;
using IntegrationAPI.DTO.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace IntegrationAPI.Controllers.Pharmacies
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private PharmacyMasterService _pharmacyMasterService;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpPost]
        public IActionResult RegisterPharmacy(PharmacyUrlDTO pharmacyUrlDto)
        {
            //TODO Ucitati ove informacije iz nekog filea - pogledati kako je u pharmacy
            Country country = new Country {Name = "Srbija"};
            City city = new City {Name = "Novi Sad", PostalCode = 21000, Country = country};
            HospitalDTO dto = new HospitalDTO
            {
                BaseUrl = $"{Request.Scheme}://{Request.Host}",
                Name = "Nasa bolnica",
                StreetName = "Vojvode Stepe",
                StreetNumber = "14",
                CityName = city.Name
            };
            RestClient client = new RestClient();
            string targetUrl = pharmacyUrlDto.BaseUrl + "/api/Registration/RegisterHospital";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(dto);
            var pharmacyString = client.Post(request).Content;
            PharmacyDTO pharmacyDto = JsonConvert.DeserializeObject<PharmacyDTO>(pharmacyString);
            Pharmacy newPaPharmacy = new Pharmacy
            {
                Name = pharmacyDto.PharmacyName,
                BaseUrl = pharmacyDto.BaseUrl,
                StreetName = pharmacyDto.StreetName,
                StreetNumber = pharmacyDto.StreetNumber,
                City = new City { PostalCode = pharmacyDto.PostalCode, Name = pharmacyDto.CityName, Country = new Country { Name = pharmacyDto.CountryName } },//TODO:POSTAL CODE
                ApiKey = pharmacyDto.ApiKey,
                GrpcSupported = pharmacyDto.GrpcSupported
            };
            _pharmacyMasterService.SavePharmacy(newPaPharmacy);
            return Ok();
        }
    }
}
