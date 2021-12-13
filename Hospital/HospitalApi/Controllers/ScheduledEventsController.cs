using AutoMapper;
using Hospital.Schedule.Service.ServiceInterface;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ScheduledEventsController : ControllerBase
    {
        private readonly IScheduledEventsService eventsService;
        private readonly IMapper mapper;

        public ScheduledEventsController(IScheduledEventsService eventsService, IMapper mapper)
        {
            this.eventsService = eventsService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        public IActionResult GetFinishedUserEvents(int userId)
        {
            List<ScheduledEventsDTO> scheduledEventsDTOs = new List<ScheduledEventsDTO>();
            foreach (var e in eventsService.getFinishedUserEvents(userId))
            {
                ScheduledEventsDTO dto = mapper.Map<ScheduledEventsDTO>(e);
                scheduledEventsDTOs.Add(dto);
            }
            return Ok(scheduledEventsDTOs);
        }
    }
}
