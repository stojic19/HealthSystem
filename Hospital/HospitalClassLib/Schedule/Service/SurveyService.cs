using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Service
{
    public class SurveyService : ISurveyService
    {
        private readonly IUnitOfWork UoW;

        public SurveyService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }
        public void createSurvey(Survey survey)
        {
            UoW.GetRepository<ISurveyWriteRepository>().Add(survey);
        }

        public IEnumerable<Survey> getAll()
        {
            return UoW.GetRepository<ISurveyReadRepository>().GetAll();
        }

        public SurveySection getSurveySection(int id, SurveyCategory category)
        {
            SurveySection surveySection = new SurveySection()
            {
                Questions = UoW.GetRepository<IQuestionReadRepository>().GetAll()
                                                                        .Where(x => x.Category == category && x.SurveyId == id)
                                                                        .ToList(),
                Category = category

            };
            return surveySection;
        }
    }
}
