using System;
using AutoMapper;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hospital.Schedule.Service.Interfaces;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ScheduledEventController : ControllerBase
    {
        private readonly IScheduledEventService _eventsService;
        private readonly IMapper _mapper;

        public ScheduledEventController( IScheduledEventService eventsService, IMapper mapper)
        {
            this._eventsService = eventsService;
            this._mapper = mapper;
        }

        [HttpGet("{userId}")]
        public IActionResult GetFinishedUserEvents(int userId)
        {
            List<ScheduledEventsDTO> scheduledEventsDTOs = new List<ScheduledEventsDTO>();
            foreach (var e in _eventsService.getFinishedUserEvents(userId))
            {
                ScheduledEventsDTO dto = _mapper.Map<ScheduledEventsDTO>(e);
                scheduledEventsDTOs.Add(dto);
            }

            return Ok(scheduledEventsDTOs);
        }

        [HttpGet]
        public IActionResult GetAvailableAppointments([FromQuery(Name = "doctorId")] int doctorId, string preferredDate)
        {
            var preferredDateTime = DateTime.Parse(preferredDate);
            var scheduledEvents = _eventsService.GetAvailableAppointments(doctorId, preferredDateTime);
            return Ok(scheduledEvents);
        }
    }
}