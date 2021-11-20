using Hospital.Model;
using Hospital.Model.Enumerations;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IAnsweredQuestionReadRepository : IReadBaseRepository<int, AnsweredQuestion>
    {
        public double GetAvgQuestionRating(int questionId);
        public double GetAvgSectionRating(SurveyCategory surveyCategory);
        public double GetNumOfRatingForQuestion(int questionId, int rating);
    }

}
