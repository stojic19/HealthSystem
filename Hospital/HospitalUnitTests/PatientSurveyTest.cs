using HospitalUnitTests.Base;
using Xunit;
using System;
using Shouldly;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.Schedule.Repository;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.RoomsAndEquipment.Model;
using System.Collections.Generic;

namespace HospitalUnitTests
{
    public class PatientSurveyTest : BaseTest
    {
        public PatientSurveyTest(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void One_answered_surveys_created()
        {
            #region Arrange
            
            ClearDbContext();
            Survey testSurvey = new(true);
            Context.Surveys.Add(testSurvey);
          
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

            Patient testPatient = new(1, "testPatient", new MedicalRecord());
            Context.Patients.Add(testPatient);

            Doctor testDoctor = new(2, new Shift().Id, new Specialization(), new Room());
            Context.Doctors.Add(testDoctor);

            ScheduledEvent events = new(0, false, true, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day),
                 new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);
            Context.ScheduledEvents.Add(events);

            Context.AnsweredSurveys.Add(new AnsweredSurvey(new List<AnsweredQuestion>(), DateTime.Now, testSurvey.Id, testSurvey, testPatient.Id, testPatient, events.Id, events));
           
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

            Context.SaveChanges();
            #endregion

            var answeredSurvey = UoW.GetRepository<IAnsweredSurveyReadRepository>()
               .GetAll();
            answeredSurvey.ShouldNotBeNull();
            answeredSurvey.Count().ShouldBe(1);

        }
    }
}
