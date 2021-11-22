using HospitalUnitTests.Base;
using Xunit;
using Hospital.Model;
using System;
using Hospital.Model.Enumerations;
using Hospital.Repositories;
using Shouldly;
using System.Linq;
using Hospital.Services.ServiceImpl;

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
                Category = SurveyCategory.DoctorSurvey,
                Text = "Pitanje 3"
            });
            Context.Questions.Add(new Question()
            {
                Id = 4,
                SurveyId = 1,
                Category = SurveyCategory.HospitalSurvey,
                Text = "Pitanje 4"
            });

            //////////////////////
            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                //TODO:  
                AnsweredDate = new DateTime()

            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 1,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 1,
                Rating = 2
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 2,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 2,
                Rating = 4
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 3,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 4,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 4,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 5,
                Rating = 3
            });
            ///////////////////////
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 6,
                Rating = 2
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 7,
                Rating = 4
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 8,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 8,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 9,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 9,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 10,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 10,
                Rating = 3
            });
            //////////////////////
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 11,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 11,
                Rating = 2
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 12,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 12,
                Rating = 4
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 13,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 13,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 14,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 14,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 15,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 15,
                Rating = 3
            });
            #endregion
            Context.SaveChanges();
            var answeredSurvey = UoW.GetRepository<IAnsweredSurveyReadRepository>()
               .GetAll();
            answeredSurvey.ShouldNotBeNull();
            answeredSurvey.Count().ShouldBe(1);

        }

        [Fact]
        public void get_number_of_unfinished_surveys()
        {
            #region
            Context.Patients.Add(new Patient()
            {

                Id = "1",
                IsBlocked = false

            });
            Context.MedicalRecords.Add(new MedicalRecord
            {
                Id = 1,


            });
            Context.HospitalTreatments.Add(new HospitalTreatment
            {
                Id = 2,
                IsDone = true,
                MedicalRecordId = 1
            });
            Context.HospitalTreatments.Add(new HospitalTreatment
            {
                Id = 3,
                IsDone = true,
                MedicalRecordId = 1
            });
            #endregion
            Context.SaveChanges();
            AnswerSurveyService answerSurveyService = new AnswerSurveyService(UoW);
            var count = answerSurveyService.getSurvey(1);

            count.ShouldBe(2);

        }



    }
}
