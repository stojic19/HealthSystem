using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class SurveyStatisticsIntegrationTests : BaseTest
    {
        public SurveyStatisticsIntegrationTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Get_survey_statistics_code_200OK()
        {
            RegisterAndLogin("Manager");
            ArrangeDatabase();
            var response = await ManagerClient.GetAsync(BaseUrl + "api/SurveyStatistics/GetSurveyStatistics");
            var responseContent = await response.Content.ReadAsStringAsync();
            var surveyStatisticsDTO = JsonConvert.DeserializeObject<SurveyStatisticDTO>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            responseContent.Length.ShouldNotBe(0);
            surveyStatisticsDTO.ShouldBeAssignableTo<SurveyStatisticDTO>();
            surveyStatisticsDTO.CategoriesStatistic.ShouldNotBe(null);
            surveyStatisticsDTO.CategoriesStatistic[0].ShouldBeAssignableTo<CategoryStatisticsDTO>();
            surveyStatisticsDTO.CategoriesStatistic[0].QuestionsStatistic.ShouldNotBe(null);
            surveyStatisticsDTO.CategoriesStatistic[0].QuestionsStatistic[0].ShouldBeAssignableTo<QuestionStatisticsDTO>();
            ClearTestData();
        }

        private void ClearTestData()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testPatientUsername");
            if (patient == null) return;

            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll().Include(mr => mr.Doctor)
                    .FirstOrDefault(x => x.Id == patient.MedicalRecordId);

                UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
            }

            var survey = UoW.GetRepository<ISurveyReadRepository>().GetAll()
                .FirstOrDefault(x => x.CreatedDate == new DateTime().AddDays(1));
            if (survey != null)
            {
                UoW.GetRepository<ISurveyWriteRepository>().Delete(survey);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testDoctorUsername");
            if (doctor != null)
            {
                UoW.GetRepository<IDoctorWriteRepository>().Delete(doctor);
            }
            var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll().ToList()
                    .FirstOrDefault(x => x.Name == "TestRoom");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }
        }

        private void ArrangeDatabase()
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
               .GetAll()
               .FirstOrDefault(x => x.Name == "TestRoom");

            if (room == null)
            {
                room = new Room()
                {
                    Name = "TestRoom",
                    Description = "Room for storage",
                    Width = 7,
                    Height = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testDoctorUsername");
            if (doctor == null)
            {
                doctor = new Doctor()
                {
                    FirstName = "TestDoctor",
                    LastName = "TestDoctorLastName",
                    MiddleName = "TestDoctorMiddleName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TestDoctorStreet",
                    Specialization = new Specialization("TestSpecialization", "DescriptionSpecialization"),
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Shift = new Shift()
                    {
                        From = 8,
                        To = 4,
                        Name = "prva"
                    },
                    Room = room,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    DoctorSchedule = new DoctorSchedule()

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername");

            if (patient == null)
            {
                patient = new Patient(new MedicalRecord(null, 0, 0, doctor.Id, null))
                {
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    UserName = "testUsername",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    IsBlocked = false

                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);
            }
            var survey = UoW.GetRepository<ISurveyReadRepository>().GetAll()
                .FirstOrDefault(x => x.CreatedDate == DateTime.Now.AddDays(1));
            if (survey != null) return;
            {
                survey = new Survey(true);

                UoW.GetRepository<ISurveyWriteRepository>().Add(survey);

                var question1 = UoW.GetRepository<IQuestionReadRepository>().GetAll()
                .FirstOrDefault(x => x.Text == "TestQuestion1" && x.SurveyId == survey.Id);
                if (question1 == null)
                {
                    question1 = new Question()
                    {
                        SurveyId = survey.Id,
                        Survey = survey,
                        Text = "TestQuestion1",
                        Category = SurveyCategory.DoctorSurvey
                    };
                    UoW.GetRepository<IQuestionWriteRepository>().Add(question1);
                }
                var question2 = UoW.GetRepository<IQuestionReadRepository>().GetAll()
                    .FirstOrDefault(x => x.Text == "TestQuestion2" && x.SurveyId == survey.Id);
                if (question2 == null)
                {
                    question2 = new Question()
                    {
                        SurveyId = survey.Id,
                        Survey = survey,
                        Text = "TestQuestion2",
                        Category = SurveyCategory.HospitalSurvey
                    };
                    UoW.GetRepository<IQuestionWriteRepository>().Add(question2);
                }
                var question3 = UoW.GetRepository<IQuestionReadRepository>().GetAll()
                    .FirstOrDefault(x => x.Text == "TestQuestion3" && x.SurveyId == survey.Id);
                if (question3 == null)
                {
                    question3 = new Question()
                    {
                        SurveyId = survey.Id,
                        Survey = survey,
                        Text = "TestQuestion3",
                        Category = SurveyCategory.StaffSurvey
                    };
                    UoW.GetRepository<IQuestionWriteRepository>().Add(question3);
                }
                var scheduledEvent = new ScheduledEvent(ScheduledEventType.Appointment, false, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(1), new DateTime(), patient.Id, doctor.Id, doctor);
                patient.ScheduledEvents.Add(scheduledEvent);
                UoW.GetRepository<IPatientWriteRepository>().Update(patient);


                var answeredSurvey = UoW.GetRepository<IAnsweredSurveyReadRepository>().GetAll()
                    .FirstOrDefault(x => x.AnsweredDate == DateTime.Now.AddDays(1));
                if (answeredSurvey != null) return;
                {
                    answeredSurvey = new AnsweredSurvey(new List<AnsweredQuestion>(), DateTime.Now, survey.Id, survey, patient.Id, patient, scheduledEvent.Id, scheduledEvent);
                    UoW.GetRepository<IAnsweredSurveyWriteRepository>().Add(answeredSurvey);
                    answeredSurvey.AnsweredQuestions.Add(new AnsweredQuestion()
                    {
                        AnsweredSurvey = answeredSurvey,
                        QuestionId = question1.Id,
                        Rating = 4,
                        Category = question1.Category
                    });
                    answeredSurvey.AnsweredQuestions.Add(new AnsweredQuestion()
                    {
                        AnsweredSurvey = answeredSurvey,
                        Question = question2,
                        Rating = 3,
                        Category = question2.Category
                    });
                    answeredSurvey.AnsweredQuestions.Add(new AnsweredQuestion()
                    {
                        AnsweredSurvey = answeredSurvey,
                        Question = question3,
                        Rating = 4,
                        Category = question3.Category
                    });
                    UoW.GetRepository<IAnsweredSurveyWriteRepository>().Update(answeredSurvey);
                }

                
            }

            

        }
    }
}