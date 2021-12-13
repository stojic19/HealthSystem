using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.AutoMapperProfiles;
using HospitalApi.DTOs;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyStatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SurveyStatisticsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSurveyStatistics()
        {
            var myMapper = new SurveyStatisticsMapper();
            var service = new SurveyStatisticsService(_uow);
            var survey = _uow.GetRepository<ISurveyReadRepository>().GetAll().First();
            var questionRepo = _uow.GetRepository<IQuestionReadRepository>();
            var surveyQuestions = questionRepo.GetAll().Where(x => x.SurveyId.Equals(survey.Id)).ToList();
            var questionStatistics = service.GetAverageQuestionRatingForAllSurveyQuestions();
            var categoryStatistics = service.GetAverageQuestionRatingForAllSurveyCategories();
            SurveyStatisticDTO surveyStatisticDTO = _mapper.Map<SurveyStatisticDTO>(survey);
            List<QuestionStatisticsDTO> questionStatisticDtos = myMapper.Merge(surveyQuestions, questionStatistics);
            surveyStatisticDTO.CategoriesStatistic = myMapper.Map(questionStatisticDtos, categoryStatistics);

            return Ok(surveyStatisticDTO);
        }
    }
}
