using AutoMapper;
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

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class SurveyController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ISurveyService surveyService;
        private readonly IUnitOfWork _uow;
        public SurveyController(IMapper mapper, ISurveyService surveyService, IUnitOfWork uow)
        {

            _mapper = mapper;
            this._uow = uow;
            this.surveyService = surveyService;
        }
        [HttpGet]
        public IEnumerable<Survey> GetSurveys()
        {
            return surveyService.getAll();
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
                DoctorSection = surveyService.getSurveySection(activeSurvey.Id, SurveyCategory.DoctorSurvey),
                MedicalStaffSection = surveyService.getSurveySection(activeSurvey.Id, SurveyCategory.StaffSurvey),
                HospitalSection = surveyService.getSurveySection(activeSurvey.Id, SurveyCategory.HospitalSurvey)
            };
            return categoriesSurvey;
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult CreateSurvey(SurveyDTO surveyDTO)
        {
            surveyService.createSurvey(_mapper.Map<Survey>(surveyDTO));
            return Ok("Success");

        }
    }
}
