using AutoMapper;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public MedicalRecordController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPatientWithRecord()
        {
            var service = new MedicalRecordService(_uow);
            var patient = _mapper.Map<PatientDTO>(service.GetPatientWithRecord());
            return Ok(patient);
        }
    }
}
