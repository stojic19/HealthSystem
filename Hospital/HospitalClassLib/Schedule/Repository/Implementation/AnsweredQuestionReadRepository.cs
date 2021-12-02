using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Repository.Implementation
{
    public class AnsweredQuestionReadRepository : ReadBaseRepository<int, AnsweredQuestion>, IAnsweredQuestionReadRepository
    {
        public AnsweredQuestionReadRepository(AppDbContext context) : base(context)
        {
        }

        public List<CategoryStatistic> GetAverageQuestionRatingForAllCategories()
        {
            var categoryAverageRatings = GetAll()
                .GroupBy(t => new { ID = t.Category })
                .Select(g => new { AverageCategoryRating = g.Average(p => p.Rating), Category = g.Key.ID }).ToList();
            List<CategoryStatistic> listOfCategoryAverageRating = new List<CategoryStatistic>();
            foreach (var g in categoryAverageRatings)
            {
                CategoryStatistic dto = new CategoryStatistic
                {
                    AverageRating = g.AverageCategoryRating,
                    Category = g.Category
                };
                listOfCategoryAverageRating.Add(dto);
            }
            return listOfCategoryAverageRating;
        }

        public List<QuestionStatistic> GetAverageQuestionRatingForAllQuestions()
        {
            var questionsAverageRatings = GetAll()
                .GroupBy(t => new { ID = t.QuestionId })
                .Select(g => new { AverageQuestionRating = g.Average(p => p.Rating), QuestionId = g.Key.ID }).ToList();
            List<QuestionStatistic> listOfQuestionsAverageRating = new List<QuestionStatistic>();
            foreach (var g in questionsAverageRatings)
            {
                QuestionStatistic dto = new QuestionStatistic
                {
                    AverageRating = g.AverageQuestionRating,
                    QuestionId = g.QuestionId
                };
                listOfQuestionsAverageRating.Add(dto);
            }
            return listOfQuestionsAverageRating;
        }

        public List<RatingCount> GetNumberOfEachRatingForEachQuestion()
        {
            var ratingCounts = GetAll()
                .GroupBy(t => new { ID = t.QuestionId, RATING = t.Rating })
                .Select(g => new { RatingCount = g.Count(), Rating = g.Key.RATING, QuestionId = g.Key.ID }).ToList();
            List<RatingCount> listOfRatingCounts = new List<RatingCount>();
            foreach (var g in ratingCounts)
            {
                RatingCount dto = new RatingCount
                {
                    QuestionId = g.QuestionId,
                    Rating = g.Rating,
                    Count = g.RatingCount
                };
                listOfRatingCounts.Add(dto);
            }
            return listOfRatingCounts;
        }
    }
}
