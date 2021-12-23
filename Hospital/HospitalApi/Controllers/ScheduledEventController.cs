using System;
using AutoMapper;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Hospital.Schedule.Service.Interfaces;
using System.Linq;

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
            var eventsDTOs = _eventsService.GetFinishedUserEvents(userId).Select(e =>
              _mapper.Map<ScheduledEventsDTO>(e)).ToList();

            return Ok(eventsDTOs);
        }
        [HttpGet("{userId}")]
        public IActionResult GetEventsForSurvey(int userId)
        {
            var eventsForSurveyDTOs = _eventsService.GetEventsForSurvey(userId).Select(e =>
              _mapper.Map<EventsForSurveyDTO>(e)).ToList();

            return Ok(eventsForSurveyDTOs);
        }

        [HttpGet("{eventId}")]
        public IActionResult GetScheduledEvent(int eventId)
        {
            var eventsDTOs = _mapper.Map<ScheduledEventsDTO>(_eventsService.GetScheduledEvent(eventId));

            return Ok(eventsDTOs);
        }

        [HttpGet("{userId}")]
        public IActionResult GetCanceledUserEvents(int userId)
        {
            var eventsDTOs = _eventsService.GetCanceledUserEvents(userId).Select(e =>
              _mapper.Map<ScheduledEventsDTO>(e)).ToList();

            return Ok(eventsDTOs);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUpcomingUserEvents(int userId)
        {
            var eventsDTOs = _eventsService.GetUpcomingUserEvents(userId).Select(e =>
                _mapper.Map<ScheduledEventsDTO>(e)).ToList();

            return Ok(eventsDTOs);
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