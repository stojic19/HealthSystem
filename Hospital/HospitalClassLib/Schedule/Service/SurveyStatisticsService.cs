using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class SurveyStatisticsService
    {
        private readonly IUnitOfWork _uow;
        public SurveyStatisticsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<QuestionStatistic> GetAverageQuestionRatingForAllSurveyQuestions()
        {
            var answeredQuestionRepo = _uow.GetRepository<IAnsweredQuestionReadRepository>();
            var questionRatings = answeredQuestionRepo.GetAverageQuestionRatingForAllQuestions();
            var ratingCounts = answeredQuestionRepo.GetNumberOfEachRatingForEachQuestion();

            foreach (var v in questionRatings)
            {
                v.RatingCounts = RatingCountsForOneQuestion(ratingCounts, v.QuestionId);
            }

            return questionRatings;
        }
        public List<CategoryStatistic> GetAverageQuestionRatingForAllSurveyCategories()
        {
            var answeredQuestionRepo = _uow.GetRepository<IAnsweredQuestionReadRepository>();
            var a = answeredQuestionRepo.GetAverageQuestionRatingForAllCategories();
            return a;
        }

        public List<double> RatingCountsForOneQuestion(List<RatingCount> ratingCounts, int QuestionId)
        {
            double[] ratings = new double[5];
            foreach (var r in ratingCounts.Where(r => r.QuestionId.Equals(QuestionId)))
            {
                ratings[r.Rating - 1] = r.Count;
            }
            return ratings.ToList();
        }
    }
}
