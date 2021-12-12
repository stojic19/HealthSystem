using Hospital.Schedule.Model;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.ServiceInterface
{
    public interface IPatientSurveyService
    {
        public AnsweredSurvey createAnsweredSurvey(AnsweredSurvey answeredSurvey);
        IEnumerable<AnsweredSurvey> getAllAnsweredSurvey();
    }
}
