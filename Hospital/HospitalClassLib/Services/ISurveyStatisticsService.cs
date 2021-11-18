using Hospital.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface ISurveyStatisticsService
    {
        public double GetAvgQuestionRating(int questionId);

        public double GetAvgSectionRating(SurveyCategory surveyCategory);
        public double GetNumOfRatingForQuestion(int questionId, int rating);
    }
}
