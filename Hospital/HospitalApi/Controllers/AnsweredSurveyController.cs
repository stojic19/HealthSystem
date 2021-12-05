using AutoMapper;
using Hospital.Model;
using Hospital.Repositories.Base;
using Hospital.Services;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]

    public class AnsweredSurveyController : ControllerBase
    {
        private readonly IPatientSurveyService surveyService;
        private readonly IMapper mapper;

        public AnsweredSurveyController(IPatientSurveyService surveyService, IMapper mapper)
        {
         
            this.surveyService = surveyService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<AnsweredSurvey> GetAllAnsweredSurvey()
        {
            return surveyService.GetAllAnsweredSurvey();
        }

        [HttpPost]
        public IActionResult CreateAnsweredSurvey(AnsweredSurveyDTO answeredSurveyDTO)
        {
          
            var temp = mapper.Map<AnsweredSurvey>(answeredSurveyDTO);
            return Ok(surveyService.createAnsweredSurvey(temp));
        }


    }
}
