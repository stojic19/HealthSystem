using AutoMapper;
using AutoMapper.Configuration;
using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Schedule.Model.Wrappers;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleAppointmentController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;

        public ScheduleAppointmentController(IUnitOfWork uow,IMapper mapper,AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetRecommendedAppointments([FromQuery(Name = "doctorId")] int doctorId, string dateStart, string dateEnd, bool isDoctorPriority)
        {
            DateTime startDate = DateTime.ParseExact(dateStart, "M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(dateEnd, "M/d/yyyy", CultureInfo.InvariantCulture);
            RecommendedAppointmentService service = new(_uow, _context);
            var availableAppointments = service.GetAvailableAppointmentsForDoctorAndDateRange(doctorId, startDate, endDate);
            var retVal = new List<AvailableAppointmentDTO>();
            if (availableAppointments.ToList().Count == 0)
            {
                if (isDoctorPriority)
                {
                    ConvertToDtoList(service.GetAvailableAppointmentsForDoctorPriority(doctorId, startDate, endDate), retVal);
                    return Ok(retVal);
                }
                else
                {
                    ConvertToDtoList(service.GetAvailableAppointmentsForDatePriority(doctorId, startDate, endDate), retVal);
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
            foreach (var appointment in availableAppointments)
            {
                retVal.Add(_mapper.Map<AvailableAppointmentDTO>(appointment));
            }
        }

        [HttpPost]
        public IActionResult ScheduleAppointment([FromBody] RecommendedAppointmentDTO newAppointment)
        {
            var appointmentToCreate = _mapper.Map<ScheduledEvent>(newAppointment);
            appointmentToCreate.ScheduleEventForPatient(_uow.GetRepository<IPatientReadRepository>().GetAll().First());
            var scheduledEvent = _uow.GetRepository<IScheduledEventWriteRepository>().Add(appointmentToCreate);
            return scheduledEvent != null ? Ok(scheduledEvent) : StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!");

        }

        [HttpPost] 
        public IActionResult ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            var appointmentToAdd = _mapper.Map<ScheduledEvent>(scheduleAppointmentDTO);
            var scheduledEventWriteRepo = _uow.GetRepository<IScheduledEventWriteRepository>();
            var addedAppointment = scheduledEventWriteRepo.Add(appointmentToAdd);

            return addedAppointment == null ? StatusCode(StatusCodes.Status500InternalServerError,
                "Could not schedule appointment. Please try again.") : Ok(addedAppointment);
        }
    }
}




