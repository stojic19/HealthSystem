using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
                var doctors = _uow.GetRepository<IDoctorReadRepository>().GetAllDoctorsWithSpecialization();
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
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public IActionResult AddOrUpdateDoctorShift(DoctorShiftDTO doctorShiftDTO)
        {
            try
            {
                if (doctorShiftDTO == null)
                {
                    return BadRequest("Incorrect doctor format sent! Please try again.");
                }

                var doctorReadRepo = _uow.GetRepository<IDoctorReadRepository>();
                var doctorWriteRepo = _uow.GetRepository<IDoctorWriteRepository>();
                var doctor = doctorReadRepo.GetById(doctorShiftDTO.Id);


                if (doctor == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not find doctor in the database.");
                }
                doctor.Shift = doctorShiftDTO.Shift;

                Doctor updatedDoctor = doctorWriteRepo.Update(doctor);

                if (updatedDoctor == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update doctor!");
                }

                return Ok(updatedDoctor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }

        }

        
        [HttpPost]
        public IEnumerable<Doctor> AddDoctors(IEnumerable<Doctor> doctors)
        {
            var doctorRepo = _uow.GetRepository<IDoctorWriteRepository>();
            return doctorRepo.AddRange(doctors);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]

        public IActionResult GetDoctorsWithShift() {

            var doctorRepo = _uow.GetRepository<IDoctorReadRepository>();
            return Ok(doctorRepo.GetAll().Include(d => d.Shift));
        }

    }
}
