using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Hospital.Schedule.Service;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlockPatientController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BlockPatientController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetMaliciousPatients()
        {
            var service = new BlockingService(_uow);
            var patients = service.GetMaliciousPatients();
            var patientDTOs = patients.Select(patient => _mapper.Map<UserForBlockingDTO>(patient)).ToList();
            return Ok(patientDTOs);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public IActionResult BlockPatient(UserForBlockingDTO user)
        {
            var service = new BlockingService(_uow);
            service.BlockPatient(user.UserName);
            var patient = _uow.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.UserName == user.UserName);
            if (patient == null) return BadRequest("There is no user with username: " + user.UserName + " !");
            if (patient.IsBlocked)
            {
                var patients = service.GetMaliciousPatients();
                var patientDTOs = patients.Select(patient => _mapper.Map<UserForBlockingDTO>(patient)).ToList();
                return Ok(patientDTOs);
            }
            return BadRequest("There is no user with username: " + user.UserName + " !");
        }
    }
}
