using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ReportController(IReportService reportService, IMapper mapper, IUnitOfWork uow)
        {
            this._reportService = reportService;
            this._mapper = mapper;
            _uow = uow;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{userName}")]
        public IEnumerable<Report> GetAllReports(string userName)
        {
            return _reportService.GetAllReports(userName);
        }
       

       //[Authorize(Roles = "Patient")]
       [HttpGet]
       public ReportDTO GetReport([FromQuery(Name = "eventId")] int eventId)  
        {
            return _mapper.Map<ReportDTO>(_reportService.GetReport(eventId));
        }
    }
}
