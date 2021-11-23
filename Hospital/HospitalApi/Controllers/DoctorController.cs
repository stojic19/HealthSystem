using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Microsoft.AspNetCore.Cors;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DoctorController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            try
            {
                var doctorsRepo = _uow.GetRepository<IDoctorReadRepository>();
                var doctors = doctorsRepo.GetAll();
                return Ok(doctors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
            }

        }
    }
}
