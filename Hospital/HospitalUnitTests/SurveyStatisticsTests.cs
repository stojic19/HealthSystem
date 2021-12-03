using HospitalUnitTests.Base;
using System;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model.Enumerations;
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
        public void Correct_average_rating_for_questions()
        {
            #region

            ClearDbContext();
            Context.Surveys.Add(new Survey()
            {
                Id = 1,
                CreatedDate = new DateTime()

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

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 2,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

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
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 1,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 2,
                Rating = 1
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 4
            });

            Context.SaveChanges();
            #endregion
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            var temp = service.GetAverageQuestionRatingForAllSurveyQuestions().OrderBy(o => o.QuestionId).ToList();
            double avg1 = temp[0].AverageRating;
            double avg2 = temp[1].AverageRating;
            avg1.ShouldBe(3);
            avg2.ShouldBe(3.5);
            
        }

        [Fact]
        public void Incorrect_average_rating_for_questions()
        {
            #region

            ClearDbContext();
            Context.Surveys.Add(new Survey()
            {
                Id = 1,
                CreatedDate = new DateTime()

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

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 2,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

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
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 1,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 2,
                Rating = 1
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 4
            });

            Context.SaveChanges();
            #endregion
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            double avg = service.GetAverageQuestionRatingForAllSurveyQuestions()[2].AverageRating;
            avg.ShouldNotBe(2);
        }

        [Fact]
        public void Correct_average_rating_for_section()
        {
            #region

            ClearDbContext();
            Context.Surveys.Add(new Survey()
            {
                Id = 1,
                CreatedDate = new DateTime()

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

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 2,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

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
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 1,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 2,
                Rating = 1
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 4
            });

            Context.SaveChanges();
            #endregion
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            var category = service.GetAverageQuestionRatingForAllSurveyCategories()
                .Where(x => x.Category.Equals(SurveyCategory.DoctorSurvey)).ToList();
            var avg = category.First().AverageRating;
            avg.ShouldBe(3);
        }

        [Fact]
        public void Incorrect_average_rating_for_section()
        {
            #region

            ClearDbContext();
            Context.Surveys.Add(new Survey()
            {
                Id = 1,
                CreatedDate = new DateTime()

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

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 2,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

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
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 1,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 2,
                Rating = 1
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 4
            });

            Context.SaveChanges();
            #endregion
            SurveyStatisticsService service = new SurveyStatisticsService(UoW);
            var category = service.GetAverageQuestionRatingForAllSurveyCategories()
                .Where(x => x.Category.Equals(SurveyCategory.HospitalSurvey)).ToList();
            var avg = category.First().AverageRating;
            avg.ShouldNotBe(3);
            avg.ShouldBe(4);
        }

        [Fact]
        public void Correct_number_of_each_rating()
        {
            #region

            ClearDbContext();
            Context.Surveys.Add(new Survey()
            {
                Id = 1,
                CreatedDate = new DateTime()

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

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 1,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

            Context.AnsweredSurveys.Add(new AnsweredSurvey()
            {
                Id = 2,
                SurveyId = 1,
                PatientId = 1,
                AnsweredDate = new DateTime()

            });

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
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 5,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 1,
                Rating = 5
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 6,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 3,
                AnsweredSurveyId = 2,
                Rating = 1
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 7,
                Category = SurveyCategory.HospitalSurvey,
                QuestionId = 4,
                AnsweredSurveyId = 1,
                Rating = 4
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 8,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 1,
                AnsweredSurveyId = 1,
                Rating = 2
            });
            Context.AnsweredQuestions.Add(new AnsweredQuestion()
            {
                Id = 9,
                Category = SurveyCategory.DoctorSurvey,
                QuestionId = 1,
                AnsweredSurveyId = 2,
                Rating = 2
            });

            Context.SaveChanges();
            #endregion

            var repo = UoW.GetRepository<IAnsweredQuestionReadRepository>();
            var service = new SurveyStatisticsService(UoW);
            var ratings = repo.GetNumberOfEachRatingForEachQuestion();
            var countsForQuestion = service.RatingCountsForOneQuestion(ratings, 1);
            countsForQuestion[0].ShouldBe(0);
            countsForQuestion[1].ShouldBe(3);
            countsForQuestion[2].ShouldBe(0);
            countsForQuestion[3].ShouldBe(1);
            countsForQuestion[4].ShouldBe(0);
        }
    }
}
