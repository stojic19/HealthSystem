using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SurveyController : ControllerBase
    {

        private readonly ISurveyService surveyService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public SurveyController(IMapper mapper, ISurveyService surveyService, IUnitOfWork uow)
        {

            _mapper = mapper;
            this._uow = uow;
            this.surveyService = surveyService;
        }
        [HttpGet]
        public IEnumerable<Survey> GetSurveys()
        {
            return surveyService.GetAll();
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public CategoriesSurvey getSurveyByCategories()
        {

            var activeSurvey = _uow.GetRepository<ISurveyReadRepository>()
                                    .GetAll()
                                    .FirstOrDefault();
            CategoriesSurvey categoriesSurvey = new CategoriesSurvey()
            {
                SurveyId = activeSurvey.Id,
                DoctorSection = surveyService.GetSurveySection(activeSurvey.Id, SurveyCategory.DoctorSurvey),
                MedicalStaffSection = surveyService.GetSurveySection(activeSurvey.Id, SurveyCategory.StaffSurvey),
                HospitalSection = surveyService.GetSurveySection(activeSurvey.Id, SurveyCategory.HospitalSurvey)
            };
            return categoriesSurvey;
        }

        //[Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult CreateSurvey()
        {
            surveyService.CreateSurvey(new Survey(true));
            return Ok("Success");

        }
    }
}
