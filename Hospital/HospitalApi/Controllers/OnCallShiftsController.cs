﻿using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OnCallShiftsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public OnCallShiftsController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult FindByMonthAndWeek([FromQuery(Name = "month")] int month, [FromQuery(Name = "week")] int week)
        {

            if (month <= 0 || week <= 0)
            {
                return BadRequest();
            }

            var shiftsRepo = _uow.GetRepository<IOnCallDutyReadRepository>();
            return Ok(shiftsRepo.GetAll()
                .Where(shift => shift.Month == month && shift.Week == week).FirstOrDefault());
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult AddDoctor([FromQuery(Name = "shiftId")] int shiftId, [FromQuery(Name = "doctorId")] int doctorId)
        {

            if (shiftId <= 0 || doctorId <= 0)
            {
                return BadRequest();
            }

            var shiftsReadRepo = _uow.GetRepository<IOnCallDutyReadRepository>();
            var shiftsWriteRepo = _uow.GetRepository<IOnCallDutyWriteRepository>();
            var doctorsReadRepo = _uow.GetRepository<IDoctorReadRepository>();

            var shift = shiftsReadRepo.GetAll()
                .Include(x => x.DoctorsOnDuty)
                .Where(shift => shift.Id == shiftId).FirstOrDefault();

            var doctor = doctorsReadRepo.GetAll().Include(d => d.DoctorSchedule).Where(d => d.Id == doctorId).FirstOrDefault();
            shift.AddDoctor(doctor.DoctorSchedule);

            shiftsWriteRepo.Update(shift);

            shift = shiftsReadRepo.GetAll()
                .Where(shift => shift.Id == shiftId).FirstOrDefault();

            return Ok(shift);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult RemoveDoctor([FromQuery(Name = "shiftId")] int shiftId, [FromQuery(Name = "doctorId")] int doctorId)
        {

            if (shiftId <= 0 || doctorId <= 0)
            {
                return BadRequest();
            }

            var shiftsReadRepo = _uow.GetRepository<IOnCallDutyReadRepository>();
            var shiftsWriteRepo = _uow.GetRepository<IOnCallDutyWriteRepository>();
            var doctorsReadRepo = _uow.GetRepository<IDoctorReadRepository>();

            var shift = shiftsReadRepo.GetAll()
                        .Include(x => x.DoctorsOnDuty)
                        .Where(shift => shift.Id == shiftId).FirstOrDefault();

            var doctor = doctorsReadRepo.GetAll().Include(d => d.DoctorSchedule).Where(d => d.Id == doctorId).FirstOrDefault();
            shift.RemoveDoctor(doctor.DoctorSchedule);
            shiftsWriteRepo.Update(shift);

            shift = shiftsReadRepo.GetAll()
                .Where(shift => shift.Id == shiftId).FirstOrDefault();

            return Ok(shift);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetDoctors([FromQuery(Name = "shiftId")] int shiftId)
        {

            if (shiftId <= 0)
            {
                return BadRequest();
            }

            var shiftsReadRepo = _uow.GetRepository<IOnCallDutyReadRepository>();
            var shiftsWriteRepo = _uow.GetRepository<IOnCallDutyWriteRepository>();
            var doctorsReadRepo = _uow.GetRepository<IDoctorReadRepository>();

            var shift = shiftsReadRepo.GetAll()
                .Include(x => x.DoctorsOnDuty)
                .Where(shift => shift.Id == shiftId).FirstOrDefault();

            List<Doctor> doctors = new List<Doctor>();

            foreach (Doctor doctor in doctorsReadRepo.GetAll()) {
                if (shift.DoctorsOnDuty.Contains(doctor.DoctorSchedule))
                    doctors.Add(doctor);
            }

            return Ok(doctors);
        }


    }
}
