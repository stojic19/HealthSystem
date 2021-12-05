using Hospital.Model;
using Hospital.Model.DataObjects;
using Hospital.Model.Enumerations;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;




namespace Hospital.Services.ServiceImpl
{
    public class PatientSurveyService : IPatientSurveyService
    {
        private IUnitOfWork UoW;

        public PatientSurveyService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

       

        public AnsweredSurvey createAnsweredSurvey(AnsweredSurvey answeredSurvey)
        {
            
           return  UoW.GetRepository<IAnsweredSurveyWriteRepository>().Add(answeredSurvey);
        }

        public void createSurvey(Survey survey)
        {
            UoW.GetRepository<ISurveyWriteRepository>().Add(survey);
          
        }

        public IEnumerable<AnsweredSurvey> GetAllAnsweredSurvey()
        {
           return  UoW.GetRepository<IAnsweredSurveyReadRepository>().GetAll().Include(x=>x.AnsweredQuestions);
        }

        public SurveySection getSurveySection(int id, SurveyCategory category)
        {

            SurveySection surveySection = new SurveySection()
            {
                questions = UoW.GetRepository<IQuestionReadRepository>().GetAll()
                                                                        .Where(x => x.Category == category && x.SurveyId == id)
                                                                        .ToList(),
                category = category

            };
            return surveySection;
        }
    }
}
