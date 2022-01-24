using System.Net;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using IntegrationAPI.Controllers.Base;
using IntegrationAPI.DTO.Pharmacies;
using IntegrationAPI.DTO.Shared;
using IntegrationAPI.HttpRequestSenders;
using IntegrationApi.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace IntegrationAPI.Controllers.Pharmacies
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class RegistrationController : BaseIntegrationController
    {
        private PharmacyMasterService _pharmacyMasterService;

        public RegistrationController(IUnitOfWork unitOfWork, IHttpRequestSender httpRequestSender) : base(unitOfWork, httpRequestSender)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpPost, Produces("application/json")]
        public IActionResult RegisterPharmacy(PharmacyUrlDTO pharmacyUrlDto)
        {
            var response = _httpRequestSender.Post(pharmacyUrlDto.BaseUrl + "/api/Registration/RegisterHospital", _hospitalInfo);
            if (response.StatusCode != HttpStatusCode.OK) return BadRequest(PharmacyMessages.CannotReach);
            var pharmacyString = response.Content;
            PharmacyDTO pharmacyDto = JsonConvert.DeserializeObject<PharmacyDTO>(pharmacyString);
            Location location = new Location(pharmacyDto.Latitude, pharmacyDto.Longitude);
            var newPharmacy = CreatePharmacy(pharmacyDto, location);
            _pharmacyMasterService.SavePharmacy(newPharmacy);
            return Ok(PharmacyMessages.Registered);
        }

        private static Pharmacy CreatePharmacy(PharmacyDTO pharmacyDto, Location location)
        {
            Pharmacy newPharmacy = new Pharmacy
            {
                Name = pharmacyDto.PharmacyName,
                BaseUrl = pharmacyDto.BaseUrl,
                StreetName = location.GetStreetName(),
                StreetNumber = location.GetHouseNumber(),
                City = location.GetCity(),
                ApiKey = pharmacyDto.ApiKey,
                GrpcSupported = pharmacyDto.GrpcSupported,
                Location = new Location(pharmacyDto.Latitude, pharmacyDto.Longitude),
                Email = pharmacyDto.Email
            };
            return newPharmacy;
        }
    }
}
