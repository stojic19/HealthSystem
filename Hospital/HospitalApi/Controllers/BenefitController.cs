using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HospitalApi.DTOs;
using HospitalApi.HttpRequestSenders;
using Newtonsoft.Json;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BenefitController : ControllerBase
    {
        private readonly IHttpRequestSender _httpRequestSender;
        private readonly string _integrationBaseUrl;

        public BenefitController(IHttpRequestSender httpRequestSender)
        {
            _httpRequestSender = httpRequestSender;
            _integrationBaseUrl = "https://localhost:44302";
        }

        [HttpGet]
        public IActionResult GetBenefits()
        {
            var result = _httpRequestSender.Get( _integrationBaseUrl +"/api/Benefit/GetRelevantBenefits");
            var benefits = JsonConvert.DeserializeObject<List<BenefitDTO>>(result.Content);
            if (result.StatusCode != HttpStatusCode.OK) return NoContent();
            return Ok(benefits);
        }
    }
}
