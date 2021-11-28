using Hospital.SharedModel.Model.Enumerations;

namespace Hospital.Schedule.Services
{
    public interface ISurveyStatisticsService
    {
        public double GetAvgQuestionRating(int questionId);

        public double GetAvgSectionRating(SurveyCategory surveyCategory);
        public double GetNumOfRatingForQuestion(int questionId, int rating);
    }
}
