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
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Enumerations;

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
        public IActionResult GetDoctorsWithSpecialization([FromQuery(Name = "specializationName")] string specializationName)
        {
            var doctorsRepo = _uow.GetRepository<IDoctorReadRepository>();
            return Ok(doctorsRepo.GetSpecializedDoctors(specializationName));
            
        }

        [HttpGet]
        public IActionResult GetALlSpecializations()
        {
            return Ok(_uow.GetRepository<IDoctorReadRepository>().GetAllSpecializations());
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
                    .Include(d => d.DoctorSchedule);
                return Ok(doctors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
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
            return Ok(doctorRepo.GetAll()
                .Include(d => d.Shift));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult AddVacation(VacationDTO vacationDTO) {

            try
            {
                if (vacationDTO == null)
                {
                    return BadRequest();
                }

                var doctorRepoRead = _uow.GetRepository<IDoctorReadRepository>();
                var doctorScheduleRepoRead = _uow.GetRepository<IDoctorScheduleReadRepository>();
                var doctorScheduleRepoWrite = _uow.GetRepository<IDoctorScheduleWriteRepository>();

                var doctor = doctorRepoRead.GetById(vacationDTO.DoctorId);

                if (doctor == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update doctor!");
                }

                var doctorSchedule = doctorScheduleRepoRead.GetById(doctor.DoctorScheduleId);

                if (doctorSchedule.Vacations.Where(v => v.StartDate > DateTime.Now).Count() > 0) {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Vacation already exists!");
                }

                if (vacationDTO.StartDate < DateTime.Now || vacationDTO.EndDate < DateTime.Now || vacationDTO.StartDate > vacationDTO.EndDate) {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update doctor!");
                }

                Vacation v = new Vacation(vacationDTO.Type, vacationDTO.StartDate, vacationDTO.EndDate);

                var vacations = doctorSchedule.Vacations.ToList();
                vacations.Add(v);
                doctorSchedule.Vacations = vacations;
                doctorScheduleRepoWrite.Update(doctorSchedule);

                return Ok(doctor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }

        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        public IActionResult UpdateVacation(VacationDTO vacationDTO)
        {

            try
            {
                if (vacationDTO == null)
                {
                    return BadRequest();
                }

                DateTime startDate = vacationDTO.StartDate;
                DateTime endDate = vacationDTO.EndDate;
                VacationType type = vacationDTO.Type;
                Vacation v = new Vacation(type, startDate, endDate);

                var doctorRepoRead = _uow.GetRepository<IDoctorReadRepository>();
                var doctorScheduleRepoRead = _uow.GetRepository<IDoctorScheduleReadRepository>();
                var doctorScheduleRepoWrite = _uow.GetRepository<IDoctorScheduleWriteRepository>();

                var doctor = doctorRepoRead.GetById(vacationDTO.DoctorId);

                if (doctor == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update doctor!");
                }

                var doctorSchedule = doctorScheduleRepoRead.GetById(doctor.DoctorScheduleId);

                var futureVacation = doctorSchedule.Vacations.Where(v => v.StartDate > DateTime.Now).FirstOrDefault();

                var vacations = doctorSchedule.Vacations.ToList();
                vacations.Remove(futureVacation);
                vacations.Add(v);
                doctorSchedule.Vacations = vacations;
                doctorScheduleRepoWrite.Update(doctorSchedule);

                return Ok(doctor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in database!");
            }

        }


        [Authorize(Roles = "Manager")]
        [HttpDelete]
        public IActionResult DeleteVacation([FromQuery(Name = "id")] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var doctorReadRepo = _uow.GetRepository<IDoctorReadRepository>();
            var doctorScheduleRepoRead = _uow.GetRepository<IDoctorScheduleReadRepository>();
            var doctorScheduleRepoWrite = _uow.GetRepository<IDoctorScheduleWriteRepository>();

            var doctor = doctorReadRepo.GetById(id);

            if (doctor == null) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't update doctor!");
            }

            var doctorSchedule = doctorScheduleRepoRead.GetById(doctor.DoctorScheduleId);

            var futureVacation = doctorSchedule.Vacations.Where(v => v.StartDate > DateTime.Now).FirstOrDefault();

            var vacations = doctorSchedule.Vacations.ToList();
            vacations.Remove(futureVacation);
            doctorSchedule.Vacations = vacations;
            doctorScheduleRepoWrite.Update(doctorSchedule);

            return Ok();

        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetFutureVacations() {

            var doctorRepo = _uow.GetRepository<IDoctorReadRepository>();
            return Ok(doctorRepo.GetAll()
                .Include(d => d.DoctorSchedule).ThenInclude(ds => ds.Vacations.Where(v => v.StartDate > DateTime.Now)));
                //.Include(d => d.Vacations.Where(v => v.StartDate > DateTime.Now)));

        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetDoctorWithFutureVacations([FromQuery(Name = "id")] int id)
        {
            var doctorRepo = _uow.GetRepository<IDoctorReadRepository>();
            return Ok(doctorRepo.GetAll().Where(d => d.Id == id)
                .Include(d => d.DoctorSchedule).ThenInclude(ds => ds.Vacations.Where(v => v.StartDate > DateTime.Now)));
        }

    }
}
