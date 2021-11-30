using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IAnsweredQuestionReadRepository : IReadBaseRepository<int, AnsweredQuestion>
    {
        public List<QuestionStatistic> GetAverageQuestionRatingForAllQuestions();
        public List<CategoryStatistic> GetAverageQuestionRatingForAllCategories();
        public List<RatingCount> GetNumberOfEachRatingForEachQuestion();
    }

}
