using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleAppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ScheduleAppointmentController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult ScheduleAppointment(ScheduleAppointmentDTO scheduleAppointmentDTO)
        {
            var appointmentToAdd = _mapper.Map<ScheduledEvent>(scheduleAppointmentDTO);
            appointmentToAdd.RoomId =
                _uow.GetRepository<IDoctorReadRepository>().GetById(scheduleAppointmentDTO.DoctorId).RoomId;
            var scheduledEventWriteRepo = _uow.GetRepository<IScheduledEventWriteRepository>();
            var addedAppointment = scheduledEventWriteRepo.Add(appointmentToAdd);

            return addedAppointment == null ? StatusCode(StatusCodes.Status500InternalServerError,
                "Could not schedule appointment. Please try again.") : Ok(addedAppointment);
        }
    }
}
