using System.Linq;
using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Shared_model.Model.Enumerations;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class AnsweredQuestionReadRepository : ReadBaseRepository<int, AnsweredQuestion>, IAnsweredQuestionReadRepository
    {
        public AnsweredQuestionReadRepository(AppDbContext context) : base(context)
        {
        }

        public double GetAvgQuestionRating(int questionId)
        {
            return GetAll().Where(x => x.QuestionId == questionId).Average(b => b.Rating);
        }

        public double GetAvgSectionRating(SurveyCategory surveyCategory)
        {
            return GetAll().Where(x => x.Category == surveyCategory).Average(b => b.Rating);
        }

        public double GetNumOfRatingForQuestion(int questionId, int rating)
        {
            return GetAll().Where(x => x.Rating == rating && x.QuestionId == questionId).Count();
        }
    }
}
