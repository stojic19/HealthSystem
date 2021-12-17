using System;
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
            ArrangeDatabase();
            var response = await Client.GetAsync(BaseUrl + "api/SurveyStatistics/GetSurveyStatistics");
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
                .FirstOrDefault(x => x.CreatedDate == new DateTime());
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
            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization != null)
            {
                UoW.GetRepository<ISpecializationWriteRepository>().Delete(specialization);
            }
            var city = UoW.GetRepository<ICityReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCity");

            if (city != null)
            {
                UoW.GetRepository<ICityWriteRepository>().Delete(city);
            }

            var country = UoW.GetRepository<ICountryReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCountry");

            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
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
            var country = UoW.GetRepository<ICountryReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestCountry");
            if (country == null)
            {
                country = new Country()
                {
                    Name = "TestCountry",
                };
                UoW.GetRepository<ICountryWriteRepository>().Add(country);
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
            if (city == null)
            {
                city = new City()
                {
                    Name = "TestCity",
                    PostalCode = 00000,
                    Country = country

                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);
            }
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

            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization == null)
            {
                specialization = new Specialization()
                {
                    Description = "DescriptionSpecialization",
                    Name = "TestSpecialization"
                };
                UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);
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
                    SpecializationId = specialization.Id,
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Room = room,
                    City = city

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testPatientUsername");

            if (patient == null)
            {
                var medicalRecord = new MedicalRecord
                {
                    Weight = 70,
                    Height = 168,
                    BloodType = BloodType.ABNegative,
                    JobStatus = JobStatus.Student,
                    Doctor = doctor

                };
                patient = new Patient()
                {
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    UserName = "testPatientUsername",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    MedicalRecord = medicalRecord,
                    City = city,
                    IsBlocked = false

                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);
                
            }

            var date = new DateTime();
            var survey = UoW.GetRepository<ISurveyReadRepository>().GetAll()
                .FirstOrDefault(x => x.CreatedDate == date);
            if (survey != null) return;
            {
                survey = new Survey()
                {
                    CreatedDate = date
                    
                };

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
                
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == doctor.Id);
                if (scheduledEvent != null) return;
                {
                    scheduledEvent = new ScheduledEvent()
                    {
                        StartDate = date,
                        Doctor = doctor,
                        EndDate = date,
                        CancellationDate = date,
                        IsCanceled = false,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    

                    var answeredSurvey = UoW.GetRepository<IAnsweredSurveyReadRepository>().GetAll()
                        .FirstOrDefault(x => x.AnsweredDate == date);
                    if (answeredSurvey != null) return;
                    {
                        answeredSurvey = new AnsweredSurvey()
                        {
                            AnsweredDate = date.AddDays(3),
                            PatientId = patient.Id,
                            ScheduledEvent = scheduledEvent,
                            Survey = survey
                        };
                        UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
                        UoW.GetRepository<IAnsweredSurveyWriteRepository>().Add(answeredSurvey);

                        var answeredQuestion1 = UoW.GetRepository<IAnsweredQuestionReadRepository>().GetAll()
                            .FirstOrDefault(x => x.AnsweredSurveyId == answeredSurvey.Id && x.QuestionId == question1.Id);
                        if (answeredQuestion1 == null)
                        {
                            answeredQuestion1 = new AnsweredQuestion()
                            {
                                AnsweredSurvey = answeredSurvey,
                                QuestionId = question1.Id,
                                Rating = 4,
                                Category = question1.Category
                            };
                            UoW.GetRepository<IAnsweredQuestionWriteRepository>().Add(answeredQuestion1);
                        }
                        var answeredQuestion2 = UoW.GetRepository<IAnsweredQuestionReadRepository>().GetAll()
                            .FirstOrDefault(x => x.AnsweredSurveyId == answeredSurvey.Id && x.QuestionId == question2.Id);
                        if (answeredQuestion2 == null)
                        {
                            answeredQuestion2 = new AnsweredQuestion()
                            {
                                AnsweredSurvey = answeredSurvey,
                                Question = question2,
                                Rating = 3,
                                Category = question2.Category
                            };
                            UoW.GetRepository<IAnsweredQuestionWriteRepository>().Add(answeredQuestion2);
                        }
                        var answeredQuestion3 = UoW.GetRepository<IAnsweredQuestionReadRepository>().GetAll()
                            .FirstOrDefault(x => x.AnsweredSurveyId == answeredSurvey.Id && x.QuestionId == question3.Id);
                        if (answeredQuestion3 != null) return;
                        answeredQuestion3 = new AnsweredQuestion()
                        {
                            AnsweredSurvey = answeredSurvey,
                            Question = question3,
                            Rating = 4,
                            Category = question3.Category
                        };
                        UoW.GetRepository<IAnsweredQuestionWriteRepository>().Add(answeredQuestion3);
                    }
                }
            }
        }
    }
}
