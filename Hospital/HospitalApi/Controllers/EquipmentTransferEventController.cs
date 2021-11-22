using Hospital.Repositories.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTransferEventController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public EquipmentTransferEventController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public IActionResult GetAvailableTerms(AvailableTermDTO availableTermsDTO)
        {
            return Ok();
        }
    }
}
