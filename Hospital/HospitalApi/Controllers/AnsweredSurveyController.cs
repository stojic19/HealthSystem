using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.Schedule.Model;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Hospital.SharedModel.Repository.Base;
using Hospital.MedicalRecords.Repository;
using System.Linq;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class AnsweredSurveyController : ControllerBase
    {
        private readonly IPatientSurveyService surveyService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork _uow;
        public AnsweredSurveyController(IPatientSurveyService surveyService, IMapper mapper, IUnitOfWork uow)
        {
            this.surveyService = surveyService;
            this.mapper = mapper;
            this._uow = uow;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IEnumerable<AnsweredSurvey> GetAllAnsweredSurvey()
        {
            return surveyService.getAllAnsweredSurvey();
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("{username}")]
        public IActionResult CreateAnsweredSurvey(string username, [FromBody] AnsweredSurveyDTO answeredSurveyDTO)
        {
            answeredSurveyDTO.PatientId = _uow.GetRepository<IPatientReadRepository>()
                                    .GetAll()
                                    .FirstOrDefault(x => x.UserName == username ).Id;
            var temp = mapper.Map<AnsweredSurvey>(answeredSurveyDTO);
           
            return Ok(surveyService.createAnsweredSurvey(temp));
        }
    }
}
