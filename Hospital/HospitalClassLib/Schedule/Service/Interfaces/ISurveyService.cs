using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.ServiceInterface
{
    public interface ISurveyService
    {
        public void createSurvey(Survey survey);
        public SurveySection getSurveySection(int id, SurveyCategory category);
        public IEnumerable<Survey> getAll();

    }
}
