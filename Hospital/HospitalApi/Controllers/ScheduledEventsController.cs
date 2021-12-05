using AutoMapper;
using Hospital.Repositories.Base;
using Hospital.Services;
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

        private readonly IUnitOfWork uow;
        private readonly IScheduledEventsService eventsService;
        private readonly IMapper mapper;

        public ScheduledEventsController(IUnitOfWork uow, IScheduledEventsService eventsService, IMapper mapper)
        {
            this.uow = uow;
            this.eventsService = eventsService;
            this.mapper = mapper;
        }


      
        [HttpGet("{id}")]
        public IActionResult GetFinishedUserEvents(int id)
        {
 
            List<ScheduledEventsDTO> scheduledEventsDTOs = new List<ScheduledEventsDTO>();

            foreach (var e in eventsService.getFinishedUserEvents(id))
            {
                ScheduledEventsDTO dto = mapper.Map<ScheduledEventsDTO>(e);
                scheduledEventsDTOs.Add(dto);
                
            }
            return Ok(scheduledEventsDTOs);
        }


    }
}
