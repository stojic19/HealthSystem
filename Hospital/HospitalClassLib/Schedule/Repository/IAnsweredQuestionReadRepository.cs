using Hospital.Schedule.Model;
using Hospital.Shared_model.Model.Enumerations;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IAnsweredQuestionReadRepository : IReadBaseRepository<int, AnsweredQuestion>
    {
        public double GetAvgQuestionRating(int questionId);
        public double GetAvgSectionRating(SurveyCategory surveyCategory);
        public double GetNumOfRatingForQuestion(int questionId, int rating);
    }

}
