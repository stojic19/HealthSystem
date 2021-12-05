using AutoMapper;
using Hospital.Model;
using Hospital.Model.DataObjects;
using Hospital.Model.Enumerations;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Hospital.Services;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SurveyController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPatientSurveyService surveyService;

        public SurveyController(IUnitOfWork uow, IMapper mapper, IPatientSurveyService surveyService)
        {
            this._uow = uow;
            _mapper = mapper;
            this.surveyService = surveyService;
        }
        [HttpGet]
        public IEnumerable<Survey> GetSurveys()
        {
            var surveyReadRepo = _uow.GetRepository<ISurveyReadRepository>();
            return surveyReadRepo.GetAll().Include(x => x.Questions);

        }
        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            var surveyReadRepo = _uow.GetRepository<IQuestionReadRepository>();
            return surveyReadRepo.GetAll().Include(x=>x.Survey);

        }

        //TODO : Vidi mapperom
        [HttpGet("{Id}")]
        public CategoriesSurvey getSurveyByCategories(int Id)
        {
            CategoriesSurveyDTO categoriesSurveyDTO = new CategoriesSurveyDTO();
            CategoriesSurvey categoriesSurvey = new CategoriesSurvey()
            {
                SurveyId = Id,
                doctorSection = surveyService.getSurveySection(Id, SurveyCategory.DoctorSurvey),
                medicalStaffSection = surveyService.getSurveySection(Id, SurveyCategory.StaffSurvey),
                hospitalSection = surveyService.getSurveySection(Id, SurveyCategory.HospitalSurvey)
            };
            
            return categoriesSurvey;
        }

        [HttpPost]
        public IActionResult CreateSurvey(SurveyDTO surveyDTO)

        {
            Survey survey = _mapper.Map<Survey>(surveyDTO);
            surveyService.createSurvey(survey);

            return Ok("Success");

        }
    }
}
