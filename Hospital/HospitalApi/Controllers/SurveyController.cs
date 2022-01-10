using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Model.Enumerations;
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

        private readonly ISurveyService surveyService;

        public SurveyController( ISurveyService surveyService)
        {

            this.surveyService = surveyService;
        }
        [HttpGet]
        public IEnumerable<Survey> GetSurveys()
        {
            return surveyService.GetAll();
        }
     
        //[Authorize(Roles = "Patient")]
        [HttpGet("{SurveyId}")]
        public CategoriesSurvey getSurveyByCategories(int SurveyId)
        {
            CategoriesSurvey categoriesSurvey = new CategoriesSurvey()
            {
                SurveyId = SurveyId,
                DoctorSection = surveyService.GetSurveySection(SurveyId, SurveyCategory.DoctorSurvey),
                MedicalStaffSection = surveyService.GetSurveySection(SurveyId, SurveyCategory.StaffSurvey),
                HospitalSection = surveyService.GetSurveySection(SurveyId, SurveyCategory.HospitalSurvey)
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
