using AutoMapper;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Service.Interfaces;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleAppointmentController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IScheduleAppointmentService _scheduleAppointmentService;

        public ScheduleAppointmentController(IUnitOfWork uow, IMapper mapper, IScheduleAppointmentService scheduleAppointmentService)
        {
            _uow = uow;
            _mapper = mapper;
            _scheduleAppointmentService = scheduleAppointmentService;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult GetRecommendedAppointments([FromQuery(Name = "doctorId")] int doctorId, string dateStart, string dateEnd, bool isDoctorPriority)
        {
            var startDate = DateTime.ParseExact(dateStart, "M/d/yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(dateEnd, "M/d/yyyy", CultureInfo.InvariantCulture);
            var timePeriod = new TimePeriod(startDate, endDate);
            var availableAppointments = _scheduleAppointmentService.GetAvailableAppointmentsForDoctorAndDateRange(doctorId, timePeriod);
            var retVal = new List<AvailableAppointmentDTO>();
            if (availableAppointments.ToList().Count == 0)
            {
                if (isDoctorPriority)
                {
                    ConvertToDtoList(_scheduleAppointmentService.GetAvailableAppointmentsForDoctorPriority(doctorId, timePeriod), retVal);
                    return Ok(retVal);
                }
                else
                {
                    ConvertToDtoList(_scheduleAppointmentService.GetAvailableAppointmentsForDatePriority(doctorId,timePeriod), retVal);
                    return Ok(retVal);
                }
            }
            else
            {
                ConvertToDtoList(availableAppointments, retVal);
                return Ok(retVal);
            }
        }

        private void ConvertToDtoList(IEnumerable<AvailableAppointment> availableAppointments, List<AvailableAppointmentDTO> retVal)
        {
            retVal.AddRange(availableAppointments.Select(appointment => _mapper.Map<AvailableAppointmentDTO>(appointment)));
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("{userName}")]
        public IActionResult ScheduleRecommendedAppointment([FromBody] RecommendedAppointmentDTO newAppointment, string userName)
        {
            var loggedInPatient = _uow.GetRepository<IPatientReadRepository>().GetAll()
                .First(x => x.UserName == userName);
            newAppointment.RoomId =
                _uow.GetRepository<IDoctorReadRepository>().GetDoctor(newAppointment.DoctorId).RoomId;
            newAppointment.PatientId = loggedInPatient.Id;
            var addedAppointment = loggedInPatient.ScheduleAppointment(_mapper.Map<ScheduledEvent>(newAppointment));
            _uow.GetRepository<IPatientWriteRepository>().Update(loggedInPatient);
            return addedAppointment != null ? Ok(addedAppointment) : StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!");
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            var loggedInPatient = _uow.GetRepository<IPatientReadRepository>().GetAll()
                .First(x => x.UserName == scheduleAppointmentDTO.PatientUsername);
            scheduleAppointmentDTO.PatientId = loggedInPatient.Id; 
            var addedAppointment = loggedInPatient.ScheduleAppointment(_mapper.Map<ScheduledEvent>(scheduleAppointmentDTO));
            _uow.GetRepository<IPatientWriteRepository>().Update(loggedInPatient);
            return addedAppointment == null ? StatusCode(StatusCodes.Status500InternalServerError,
                "Could not schedule appointment. Please try again.") : Ok(addedAppointment);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult GetAvailableAppointments([FromQuery(Name = "doctorId")] int doctorId, string preferredDate)
        {
            var preferredDateTime = DateTime.Parse(preferredDate);
            var availableTerms = _scheduleAppointmentService.GetAvailableTermsForDoctorAndDate(doctorId, preferredDateTime);
            return Ok(availableTerms);
        }
    }
}




