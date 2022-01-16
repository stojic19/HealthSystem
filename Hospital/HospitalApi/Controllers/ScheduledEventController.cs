using AutoMapper;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Hospital.Schedule.Service.Interfaces;
using System.Linq;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ScheduledEventController : ControllerBase
    {
        private readonly IScheduledEventService _eventsService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ScheduledEventController( IScheduledEventService eventsService, IMapper mapper, IUnitOfWork uow)
        {
            this._eventsService = eventsService;
            this._mapper = mapper;
            _uow = uow;
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
        [HttpGet]
        public IActionResult CancelAppointment([FromQuery(Name = "eventId")] int eventId, [FromQuery(Name = "username")]  string username)
        {
            var loggedInPatient = _uow.GetRepository<IPatientReadRepository>().GetAll().Include(p => p.ScheduledEvents).First(p => p.UserName == username);
            loggedInPatient.CancelAppointment(eventId);
            return Ok(_uow.GetRepository<IPatientWriteRepository>().Update(loggedInPatient));
        }

    }
}