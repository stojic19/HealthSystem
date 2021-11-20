using Hospital.Model.Enumerations;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class SurveyStatisticsService : ISurveyStatisticsService
    {
        private readonly IUnitOfWork _uow;
        public SurveyStatisticsService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public double GetAvgQuestionRating(int questionId)
        {
            var answeredQuestionRepo = _uow.GetRepository<IAnsweredQuestionReadRepository>();
            return answeredQuestionRepo.GetAvgQuestionRating(questionId);
        }
        public double GetAvgSectionRating(SurveyCategory surveyCategory)
        {
            var answeredQuestionRepo = _uow.GetRepository<IAnsweredQuestionReadRepository>();
            return answeredQuestionRepo.GetAvgSectionRating(surveyCategory);
        }
        public double GetNumOfRatingForQuestion(int questionId, int rating)
        {
            var answeredQuestionRepo = _uow.GetRepository<IAnsweredQuestionReadRepository>();   
            return answeredQuestionRepo.GetNumOfRatingForQuestion(questionId,rating);
        }
    }
}
