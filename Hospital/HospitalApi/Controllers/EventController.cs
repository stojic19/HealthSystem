using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.EventStoring.Model;
using Hospital.EventStoring.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public EventController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = uow.GetRepository<IStoredEventReadRepository>().GetAll();
            return Ok(events);
        }

        [HttpPost]
        public IActionResult AddEvent(EventDTO newEvent)
        {
            var incomingEvent = new StoredEvent()
            {
                StateData = newEvent.StateData,
                Time = DateTime.Now,
                UserId = newEvent.UserId
            };

            uow.GetRepository<IStoredEventWriteRepository>().Add(incomingEvent);

            return Ok();
        }
    }
}
