using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduledEventController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ScheduledEventController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAvailableAppointments([FromQuery(Name = "doctorId")] int doctorId, string preferredDate)
        {
            try
            {

                var preferredDateTime = DateTime.Parse(preferredDate);

                var eventsRepo = _uow.GetRepository<IScheduledEventReadRepository>();
                var scheduledEvents = eventsRepo.GetAvailableAppointments(doctorId, preferredDateTime);
                return Ok(scheduledEvents);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading appointments!");
            }
        }
    }
}
