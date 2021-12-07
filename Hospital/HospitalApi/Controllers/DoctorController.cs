﻿using System;
using System.Linq;
using AutoMapper;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DoctorController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNonOverloadedDoctors()
        {
            try
            {
                var doctorsRepo = _uow.GetRepository<IDoctorReadRepository>();
                var doctors = doctorsRepo.GetNonOverloadedDoctors();
                return Ok(doctors);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error!Failed loading doctors!");
            }
        }
    }
}