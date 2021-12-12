using AutoMapper;
using AutoMapper.Configuration;
using Hospital.Database.EfStructures;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendedAppointmentController : ControllerBase
    {
        /*private readonly IConfiguration _config;
        private readonly IMapper _mapper;*/
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;

        public RecommendedAppointmentController(IUnitOfWork uow,/* IConfiguration config, IMapper mapper,*/ AppDbContext context)
        {
            _uow = uow;
            //_config = config;
            //_mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetRecommendedAppointments([FromQuery] int doctorId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] bool isDoctorPriority)
        {
            RecommendedAppointmentService service = new RecommendedAppointmentService(_uow, _context);
            var availableAppointments = service.GetAvailableAppointmentsForDoctorAndDateRange(doctorId, startDate, endDate);
            if (availableAppointments == null)
            {
                if (isDoctorPriority)
                {
                    var availableAppointmentsDp = service.GetAvailableAppointmentsForDoctorPriority(doctorId, startDate, endDate);
                    return Ok(availableAppointmentsDp);
                }
                else
                {
                    var availableAppointmentsRp = service.GetAvailableAppointmentsForDatePriority(doctorId, startDate, endDate);
                    return Ok(availableAppointmentsRp);
                }
            }
            return Ok(availableAppointments);
        }
    }
}
