using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorsReportController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public DoctorsReportController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IEnumerable<OnCallDuty> GetAllDuties()
        {
            var duties = _uow.GetRepository<IOnCallDutyReadRepository>()
                .GetAll().Include(d => d.DoctorsOnDuty);

            return duties;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult GetReportInformation(ReportDTO reportDto)
        {
            try
            {
                TimePeriod timePeriod = new TimePeriod(reportDto.Start, reportDto.End);
                var reportService = new GeneratingDoctorsReportService(_uow);
                var report = reportService.GetReportInformation(timePeriod, reportDto.DoctorsId);
                return Ok(report);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
            }


        }
    }
}
