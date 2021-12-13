using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hospital.Schedule.Service
{
    public class PatientSurveyService : IPatientSurveyService
    {
        private readonly IUnitOfWork UoW;

        public PatientSurveyService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public AnsweredSurvey createAnsweredSurvey(AnsweredSurvey answeredSurvey)
        {
            answeredSurvey.PatientId = 1;
            return UoW.GetRepository<IAnsweredSurveyWriteRepository>().Add(answeredSurvey);
        }

        public IEnumerable<AnsweredSurvey> getAllAnsweredSurvey()
        {
            return UoW.GetRepository<IAnsweredSurveyReadRepository>().GetAll().Include(x => x.AnsweredQuestions);
        }

    }
}
