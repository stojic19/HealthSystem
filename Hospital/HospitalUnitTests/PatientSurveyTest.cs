using HospitalUnitTests.Base;
using Xunit;
using System;
using Shouldly;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.Schedule.Repository;

namespace HospitalUnitTests
{
    public class PatientSurveyTest : BaseTest
    {
        public PatientSurveyTest(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void one_answered_surveys_created()
        {
            ClearDbContext();
            #region
            Context.Surveys.Add(new Survey()
            {
                Id = 1,
                CreatedDate = DateTime.Now,

            });
            Context.Questions.Add(new Question()
            {
                Id = 1,
                SurveyId = 1,
                Category = SurveyCategory.DoctorSurvey,
                Text = "Pitanje 1"
            });
            Context.Questions.Add(new Question()
            {
                Id = 2,
                SurveyId = 1,
                Category = SurveyCategory.StaffSurvey,
                Text = "Pitanje 2"
            });

            Context.Questions.Add(new Question()
            {
                Id = 3,
                SurveyId = 1,
                Category = SurveyCategory.HospitalSurvey,
                Text = "Pitanje 3"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                IsDone = true

            });

            //////////////////////
            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                ScheduledEventId = 1,
                AnsweredDate = DateTime.Now

            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 1,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 1,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 2,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 3,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 3,
                Rating = 5
            });

            #endregion
            Context.SaveChanges();
            var answeredSurvey = UoW.GetRepository<IAnsweredSurveyReadRepository>()
               .GetAll();
            answeredSurvey.ShouldNotBeNull();
            answeredSurvey.Count().ShouldBe(1);

        }
    }
}
