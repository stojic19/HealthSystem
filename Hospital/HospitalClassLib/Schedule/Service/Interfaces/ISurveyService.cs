using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.ServiceInterface
{
    public interface ISurveyService
    {
        public void CreateSurvey(Survey survey);
        public SurveySection GetSurveySection(int id, SurveyCategory category);
        public IEnumerable<Survey> GetAll();
        Survey GetActiveSurvey();
        void Save(Survey activeSurvey);
    }
}
