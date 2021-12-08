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
        public IActionResult GetAvailableAppointments([FromQuery(Name = "doctorId")] int doctorId, string startDate, string endDate)
        {
            try
            {
                var dateRange = new TimePeriod()
                {
                    StartTime = DateTime.Parse(startDate),
                    EndTime = DateTime.Parse(endDate)
                };

                var eventsRepo = _uow.GetRepository<IScheduledEventReadRepository>();
                var scheduledEvents = eventsRepo.GetAvailableAppointments(doctorId, dateRange);
                return Ok(scheduledEvents);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading appointments!");
            }
        }
    }
}
