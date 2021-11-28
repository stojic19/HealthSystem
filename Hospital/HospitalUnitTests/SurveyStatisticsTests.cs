using Hospital.Schedule.Model;
using Hospital.Schedule.Services;
using Hospital.SharedModel.Model.Enumerations;
using HospitalUnitTests.Base;
using Shouldly;
using Xunit;

namespace HospitalUnitTests
{
    public class SurveyStatisticsTests : BaseTest
    {
        public SurveyStatisticsTests(BaseFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public void calculates_avg_question_rating()
        {


            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 1,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 1,
                AnsweredSurveyId = 1,
                Rating = 2
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 2,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 1,
                AnsweredSurveyId = 2,
                Rating = 4
            });
            Context.SaveChanges();

            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double avg = service.GetAvgQuestionRating(1);
            avg.ShouldBe(3);
        }

        [Fact]
        public void calculates_wrong_avg_question_rating()
        {
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 3,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                AnsweredSurveyId = 1,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 4,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                AnsweredSurveyId = 2,
                Rating = 4
            });

            Context.SaveChanges();
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double avg = service.GetAvgQuestionRating(2);

            avg.ShouldNotBe(4);
        }

        [Fact]
        public void calculates_avg_section_rating()
        {
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                AnsweredSurveyId = 1,
                Rating = 2
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                AnsweredSurveyId = 2,
                Rating = 4
            });

            Context.SaveChanges();
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double avg = service.GetAvgSectionRating(SurveyCategory.StaffSurvey);

            avg.ShouldBe(3);
        }

        [Fact]
        public void calculates_wrong_avg_section_rating()
        {

            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                AnsweredSurveyId = 1,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 8,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 2,
                AnsweredSurveyId = 2,
                Rating = 3
            });

            Context.SaveChanges();
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double avg = service.GetAvgSectionRating(SurveyCategory.StaffSurvey);

            avg.ShouldNotBe(4);
        }

        [Fact]
        public void calculates_num_of_specific_rating_for_question()
        {

            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 9,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 2,
                Rating = 4
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 10,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 11,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 2,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 12,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 5
            });


            Context.SaveChanges();
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double num = service.GetNumOfRatingForQuestion(4, 3);

            num.ShouldBe(2);
        }

        [Fact]
        public void calculates_wrong_num_of_specific_rating_for_question()
        {
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 13,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 5,
                AnsweredSurveyId = 2,
                Rating = 4
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 14,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 5,
                AnsweredSurveyId = 1,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 15,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 5,
                AnsweredSurveyId = 2,
                Rating = 3
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 16,
                Category = SurveyCategory.StaffSurvey,
                QuestionId = 5,
                AnsweredSurveyId = 1,
                Rating = 5
            });

            Context.SaveChanges();
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double num = service.GetNumOfRatingForQuestion(5, 5);

            num.ShouldNotBe(2);
        }
    }
}
