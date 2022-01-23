using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using IntegrationAPI.Controllers.Base;
using IntegrationApi.DTO.Advertisement;
using IntegrationAPI.HttpRequestSenders;
using Newtonsoft.Json;

namespace IntegrationApi.Controllers.Advertisement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : BaseIntegrationController
    {
        public AdvertisementController(IUnitOfWork uow, IHttpRequestSender sender) : base(uow, sender)
        {
        }

        [HttpGet, Produces("application/json")]
        public IActionResult GetAdvertisementFromPharmacies()
        {
            var pharmacies = _unitOfWork.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            List<AdvertisementDto> retVal = new List<AdvertisementDto>();
            foreach (var pharmacy in pharmacies)
            {
                var result = _httpRequestSender.Get(pharmacy.BaseUrl + "/api/Advertisement/GetAdvertisements?apiKey=" +
                                                    pharmacy.ApiKey);
                if (result.StatusCode != HttpStatusCode.OK) continue;
                var adsFromPharmacy = JsonConvert.DeserializeObject<List<AdvertisementDto>>(result.Content);
                if (adsFromPharmacy == null) continue;
                foreach (var ad in adsFromPharmacy) ad.PharmacyName = pharmacy.Name;
                retVal.AddRange(adsFromPharmacy);
            }
            return Ok(retVal);
        }
    }
}
