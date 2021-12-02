using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace HospitalApi.DTOs
{
    public class QuestionStatisticsDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public SurveyCategory Category { get; set; }
        public int SurveyId { get; set; }
        public double AverageRating { get; set; }
        public List<double> RatingCounts { get; set; }
    }
}
