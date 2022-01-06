using System;
using AutoMapper;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Hospital.Schedule.Service.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Patient")]
        [HttpGet("{userName}")]
        public IActionResult GetFinishedUserEvents(string userName)
        {
            var eventsDTOs = _eventsService.GetFinishedUserEvents(userName).Select(e =>
              _mapper.Map<ScheduledEventsDTO>(e)).ToList();

            return Ok(eventsDTOs);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet("{userName}")]
        public IActionResult GetEventsForSurvey(string userName)
        {
            var eventsForSurveyDTOs = _eventsService.GetEventsForSurvey(userName).Select(e =>
              _mapper.Map<EventsForSurveyDTO>(e)).ToList();

            return Ok(eventsForSurveyDTOs);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{eventId}")]
        public IActionResult GetScheduledEvent(int eventId)
        {
            var eventsDTOs = _mapper.Map<ScheduledEventsDTO>(_eventsService.GetScheduledEvent(eventId));

            return Ok(eventsDTOs);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{userName}")]
        public IActionResult GetCanceledUserEvents(string userName)
        {
            var eventsDTOs = _eventsService.GetCanceledUserEvents(userName).Select(e =>
              _mapper.Map<ScheduledEventsDTO>(e)).ToList();

            return Ok(eventsDTOs);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{userName}")]
        public IActionResult GetUpcomingUserEvents(string userName)
        {
            var eventsDTOs = _eventsService.GetUpcomingUserEvents(userName).Select(e =>
                _mapper.Map<ScheduledEventsDTO>(e)).ToList();

            return Ok(eventsDTOs);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet("{eventId}")]
        public IActionResult CancelAppointment(int eventId)
        {

            _eventsService.CancelAppointment(eventId);
            return Ok();
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult GetAvailableAppointments([FromQuery(Name = "doctorId")] int doctorId, string preferredDate)
        {
            var preferredDateTime = DateTime.Parse(preferredDate);
            var scheduledEvents = _eventsService.GetAvailableAppointments(doctorId, preferredDateTime);
            return Ok(scheduledEvents);
        }
    }
}