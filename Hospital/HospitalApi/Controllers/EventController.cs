using System;
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
            return Ok();
        }

        [HttpPost]
        public IActionResult AddEvent(EventDTO newEvent)
        {
            return Ok();
        }
    }
}
