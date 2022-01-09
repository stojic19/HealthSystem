using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public IEnumerable<OnCallDuty> GetAllDuties()
        {
            var duties = _uow.GetRepository<IOnCallDutyReadRepository>()
                .GetAll().Include(d => d.DoctorsOnDuty);

            return duties;
        }
    }
}
