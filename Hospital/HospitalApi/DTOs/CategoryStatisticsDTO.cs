using System.Collections.Generic;
using Hospital.SharedModel.Model.Enumerations;

namespace HospitalApi.DTOs
{
    public class CategoryStatisticsDTO
    {
        public List<QuestionStatisticsDTO> QuestionsStatistic { get; set; }
        public double AverageRating { get; set; }
        public SurveyCategory Category { get; set; }
    }
}
