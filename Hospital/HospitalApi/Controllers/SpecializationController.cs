using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public SpecializationController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var specializationReadRepository = _uow.GetRepository<ISpecializationReadRepository>();
                var specializations = specializationReadRepository.GetAll();
                return Ok(specializations);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal server error! Failed loading specializations!");
            }
        }
    }
}
