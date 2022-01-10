using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.EventStoring.Model;
using Integration.EventStoring.Repository;
using Integration.Shared.Repository.Base;
using IntegrationApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers.EventStoring
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
