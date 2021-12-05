using Hospital.Model;
using Hospital.Model.DataObjects;
using Hospital.Model.Enumerations;
using System.Collections.Generic;


namespace Hospital.Services
{
    public interface IPatientSurveyService
    {
        public AnsweredSurvey createAnsweredSurvey(AnsweredSurvey answeredSurvey);

        public void createSurvey(Survey survey);
        public SurveySection getSurveySection(int id, SurveyCategory category);
        IEnumerable<AnsweredSurvey> GetAllAnsweredSurvey();
    }
}
