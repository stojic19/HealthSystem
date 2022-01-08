using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public IActionResult GetNonOverloadedDoctors()
        {
            try
            {
                var doctorsRepo = _uow.GetRepository<IDoctorReadRepository>();
                var doctors = doctorsRepo.GetNonOverloadedDoctors();
                return Ok(doctors);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
            }
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            try
            {
                var doctors = _uow.GetRepository<IDoctorReadRepository>()
                    .GetAllDoctorsWithSpecialization();
                return Ok(doctors);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
            }
        }

        [HttpGet]
        public IActionResult GetDoctorsWithSpecialization([FromQuery(Name = "specializationId")] int specializationId)
        {
            //TODO: make a DTO for Specialization
            try
            {
                var doctorsRepo = _uow.GetRepository<IDoctorReadRepository>();
                var doctors = doctorsRepo.GetSpecializedDoctors(specializationId);
                return Ok(doctors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error! Failed loading doctors!");
            }
        }

        [HttpGet]
        public IActionResult GetAllDoctorsWithShifts()
        {
            try
            {
                var doctors = _uow.GetRepository<IDoctorReadRepository>()
                    .GetAll()
                    .Include(d => d.Specialization)
                    .Include(d => d.Shift)
                    .Include(d => d.OnCallDuties);
                return Ok(doctors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
            }
        }
    }
}
