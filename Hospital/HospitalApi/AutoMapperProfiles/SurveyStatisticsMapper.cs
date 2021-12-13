using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using HospitalApi.DTOs;

namespace HospitalApi.AutoMapperProfiles
{
    public class SurveyStatisticsMapper
    {
        public SurveyStatisticsMapper()
        {

        }

        IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Question, QuestionStatisticsDTO>();
            cfg.CreateMap<QuestionStatistic, QuestionStatisticsDTO>();
        }).CreateMapper();


        public List<QuestionStatisticsDTO> Merge(List<Question> surveyQuestions, List<QuestionStatistic> questionStatistics)
        {
            List<QuestionStatisticsDTO> questionStatisticDtos = new List<QuestionStatisticsDTO>();
            foreach (var q in surveyQuestions)
            {
                var dto = new QuestionStatisticsDTO
                {
                    Category = q.Category,
                    Id = q.Id,
                    SurveyId = q.SurveyId,
                    Text = q.Text
                };
                foreach (var s in questionStatistics.Where(s => s.QuestionId.Equals(q.Id)))
                {
                    dto.AverageRating = s.AverageRating;
                    dto.RatingCounts = s.RatingCounts;
                }
                questionStatisticDtos.Add(dto);
            }
            return questionStatisticDtos;
        }

        public List<CategoryStatisticsDTO> Map(List<QuestionStatisticsDTO> questionStatisticDtos, List<CategoryStatistic> categoryStatistics)
        {
            List<CategoryStatisticsDTO> categoryStatisticsDtos = new List<CategoryStatisticsDTO>();
            foreach (var cs in categoryStatistics)
            {
                CategoryStatisticsDTO dto = new CategoryStatisticsDTO
                {
                    AverageRating = cs.AverageRating,
                    Category = cs.Category,
                    QuestionsStatistic = questionStatisticDtos.Where(x => x.Category.Equals(cs.Category)).ToList()
                };
                categoryStatisticsDtos.Add(dto);
            }
            return categoryStatisticsDtos;
        }
    }
}
