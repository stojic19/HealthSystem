using System.Linq;
using AutoMapper;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Authorize(Roles = "Patient")]
        [HttpGet("{userName}")]
        public IActionResult GetPatientWithRecord(string userName)
        {
            var patientRepo = _uow.GetRepository<IPatientReadRepository>();
            var patient = patientRepo.GetPatient(userName);
            return Ok(_mapper.Map<PatientDTO>(patient));
        }
    }
}
