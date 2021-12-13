using System.Linq;
using AutoMapper;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
            var patientRepo = _uow.GetRepository<IPatientReadRepository>();
            var loggedInPatient = patientRepo.GetAll().First();
            var patient = patientRepo.GetPatient(loggedInPatient.Id);
            var medicalRecordRepo = _uow.GetRepository<IMedicalRecordReadRepository>();
            var medicalRecord = medicalRecordRepo.GetMedicalRecord(patient.MedicalRecordId);
            patient.MedicalRecord = medicalRecord;
            var patientWithRecord = _mapper.Map<PatientDTO>(patient);
            return Ok(patientWithRecord);
        }
    }
}
