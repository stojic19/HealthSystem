using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class PatientSurveyTests : BaseTest
    {
        public PatientSurveyTests(BaseFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public async Task Create_answered_survey_should_return_200OK()
        {

            var survey = UoW.GetRepository<ISurveyReadRepository>().GetAll().FirstOrDefault();
            if (survey == null)
            {
                survey = new Survey()
                {
                    CreatedDate = DateTime.Now
                };
            }


            var question = UoW.GetRepository<IQuestionReadRepository>().GetAll().FirstOrDefault();
            if (question == null)
            {
                question = new Question()
                {
                    Text = "How did you like our services?",
                    Category = SurveyCategory.HospitalSurvey,
                    SurveyId = survey.Id
                };
            }

            AnsweredQuestionDTO answeredQuestionHospital = new AnsweredQuestionDTO
            {
                QuestionId = question.Id,
                Rating = 5,
                Type = SurveyCategory.HospitalSurvey
            };


            List<AnsweredQuestionDTO> answeredQuestionDTOs = new List<AnsweredQuestionDTO>();
            answeredQuestionDTOs.Add(answeredQuestionHospital);

            var scheduled = UoW.GetRepository<IScheduledEventReadRepository>().GetAll().FirstOrDefault();
            if (scheduled == null)
            {
                scheduled = new ScheduledEvent()
                {
                    ScheduledEventType = ScheduledEventType.Appointment,
                    IsCanceled = false,
                    IsDone = true,
                    StartDate = new DateTime(2021, 10, 17),
                    EndDate = new DateTime(2021, 10, 17)

                };
            }


            AnsweredSurveyDTO answeredSurveyDTO = new AnsweredSurveyDTO()
            {
                questions = answeredQuestionDTOs,
                AnsweredDate = DateTime.Now,
                SurveyId = survey.Id,
                ScheduledEventId = scheduled.Id

            };

            var content = GetContent(answeredSurveyDTO);

            var response = await Client.PostAsync(BaseUrl + "api/AnsweredSurvey/CreateAnsweredSurvey", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var answeredSurveyResult = JsonConvert.DeserializeObject<AnsweredSurvey>(responseContent);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            answeredSurveyResult.AnsweredQuestions.ToList().Count.ShouldNotBe(0);

            var answeredSurveyDB = UoW.GetRepository<IAnsweredSurveyReadRepository>().GetAll().Where(x => x.ScheduledEventId == scheduled.Id).FirstOrDefault();
            answeredSurveyDB.ScheduledEvent.Id.ShouldBe(scheduled.Id);

        }

    }
}
