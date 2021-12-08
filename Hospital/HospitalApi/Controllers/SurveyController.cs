using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SurveyController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ISurveyService surveyService;

        public SurveyController(IMapper mapper, ISurveyService surveyService)
        {

            _mapper = mapper;
            this.surveyService = surveyService;
        }
        [HttpGet]
        public IEnumerable<Survey> GetSurveys()
        {
            return surveyService.getAll();
        }

        //TODO : Vidi mapperom
        [HttpGet("{SurveyId}")]
        public CategoriesSurvey getSurveyByCategories(int SurveyId)
        {
            CategoriesSurvey categoriesSurvey = new CategoriesSurvey()
            {
                SurveyId = SurveyId,
                DoctorSection = surveyService.getSurveySection(SurveyId, SurveyCategory.DoctorSurvey),
                MedicalStaffSection = surveyService.getSurveySection(SurveyId, SurveyCategory.StaffSurvey),
                HospitalSection = surveyService.getSurveySection(SurveyId, SurveyCategory.HospitalSurvey)
            };
            return categoriesSurvey;
        }

        [HttpPost]
        public IActionResult CreateSurvey(SurveyDTO surveyDTO)
        {
            surveyService.createSurvey(_mapper.Map<Survey>(surveyDTO));
            return Ok("Success");

        }
    }
}
