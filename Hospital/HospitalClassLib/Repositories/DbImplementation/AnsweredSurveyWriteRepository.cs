using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;


namespace Hospital.Repositories.DbImplementation
{
    public class AnsweredSurveyWriteRepository : WriteBaseRepository<AnsweredSurvey>, IAnsweredSurveyWriteRepository
    {
        public AnsweredSurveyWriteRepository(AppDbContext context) : base(context)
        {
        }

        public void create(IEnumerable<AnsweredQuestion> answers, Guid userId, int scheduledEventId)
        {
            AnsweredSurvey answeredSurvey = new AnsweredSurvey();
            answeredSurvey.AnsweredDate = DateTime.Now;
            answeredSurvey.AnsweredQuestions = answers;
            answeredSurvey.PatientId = userId;
            answeredSurvey.ScheduledEventId = scheduledEventId;

        }
    }
}
