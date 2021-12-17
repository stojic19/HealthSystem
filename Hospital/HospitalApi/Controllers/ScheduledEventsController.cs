using AutoMapper;
using Hospital.Schedule.Service.ServiceInterface;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ScheduledEventsController : ControllerBase
    {
        private readonly IScheduledEventsService _eventsService;
        private readonly IMapper _mapper;

        public ScheduledEventsController(IScheduledEventsService eventsService, IMapper mapper)
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
    }
}
