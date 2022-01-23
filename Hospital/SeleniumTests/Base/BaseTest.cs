using System;
using System.Linq;
using Hospital.SharedModel.Repository.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SeleniumTests.Base
{
    public abstract class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        public IUnitOfWork UoW => _fixture.UoW;
        public HttpClient Client => _fixture.Client;
        public CookieContainer CookieContainer => _fixture.CookieContainer;
        public string BaseUrl => "http://0.0.0.0:44303/";

        public void AddCookie(string name, string value, string domain)
        {
            CookieContainer.Add(new Cookie(name, value) { Domain = domain });
        }

        public StringContent GetContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content, settings: new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            }), Encoding.UTF8, "application/json");
        }

        public void RegisterUser(string role)
        {
            object user = null;
            if (role == "Patient")
            {
                user = UoW.GetRepository<IPatientReadRepository>()
                    .GetAll().FirstOrDefault(x => x.UserName.Equals("testPatientUsername"));

                if (user == null)
                {
                    var roomWriteRepo = UoW.GetRepository<IRoomWriteRepository>();
                    var doctorWiteRepo = UoW.GetRepository<IDoctorWriteRepository>();
                    var testRoom = UoW.GetRepository<IRoomReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestRoom");
                    var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().FirstOrDefault(x => x.FirstName == "TestDoctor");

                    if (testRoom == null)
                    {
                        testRoom = new Room()
                        {
                            Name = "TestRoom",
                            RoomType = RoomType.AppointmentRoom,
                            FloorNumber = 1,
                            Description = "TestDescription"

                        };
                        roomWriteRepo.Add(testRoom);
                    }

                    if (testDoctor == null)
                    {
                        testDoctor = new Doctor()
                        {
                            FirstName = "TestDoctor",
                            LastName = "TestDoctorLastName",
                            MiddleName = "TestDoctorMiddleName",
                            DateOfBirth = DateTime.Now,
                            Gender = Gender.Female,
                            Street = "TestDoctorStreet",
                            City = new City("Novi Sad", 21000, new Country("Serbia")),
                            Room = testRoom,
                            Specialization = new Specialization("TestSpecialization", "testing"),
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
                                Name = "testShift",
                                From = 8,
                                To = 4
                            }

                        };
                        doctorWiteRepo.Add(testDoctor);
                    }

                    var newPatient = new NewPatientDTO()
                    {
                        FirstName = "TestPatient",
                        MiddleName = "TestPatientMiddleName",
                        LastName = "TestPatientLastName",
                       
                        DateOfBirth = DateTime.Now,
                        Gender = Gender.Female,
                        Street = "TesPatientStreet",
                        StreetNumber = "44",
                        City = new CityDTO()
                        {
                            Name = "Novi Sad"
                        },
                        Email = "testPatient@gmail.com",
                        UserName = "testPatientUsername",
                        Password = "TestProba123",
                      
                        PhoneNumber = "testPatientPhoneNumber",
                        MedicalRecord = new NewMedicalRecordDTO()
                        {
                            DoctorId = testDoctor.Id,
                            Measurements = new MeasurementsDTO()
                            {
                                Height = 178,
                                Weight = 65
                            },
                            BloodType = BloodType.Undefined,
                            JobStatus = JobStatus.Undefined
                         
                        }
                    };

                    Client.PostAsync(BaseUrl + "api/Registration/Register", GetContent(newPatient))
                     .GetAwaiter()
                     .GetResult();
                    var createdPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.UserName.Equals("testPatientUsername"));
                    createdPatient.EmailConfirmed = true;
                    UoW.GetRepository<IPatientWriteRepository>().Update(createdPatient);
                }
            }

            else if (role == "Manager")
            {
                user = UoW.GetRepository<IManagerReadRepository>().GetAll().FirstOrDefault(x => x.UserName.Equals("testLogInManager"));
                if (user != null) return;
                {
                    user = new NewManagerDTO()
                    {
                        FirstName = "TestManager",
                        LastName = "TestManagerLastName",
                        MiddleName = "TestManagerMiddleName",
                        DateOfBirth = new DateTime(),
                        Gender = Gender.Female,
                        Street = "TestManagerStreet",
                        UserName = "testLogInManager",
                        City = new CityDTO()
                        {
                            Name = "Novi Sad"
                        },
                        Email = "testManager@gmail.com",
                        Password = "111111aA"

                    };
                    var r = Client.PostAsync(BaseUrl + "api/Registration/RegisterManager", GetContent(user))
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }

        public void DeleteDataFromDataBase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.UserName.Equals("testPatientUsername"));
            UoW.GetRepository<IPatientWriteRepository>().Delete(patient, true);
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
           
            var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll().ToList()
                    .FirstOrDefault(x => x.Name == "TestRoom");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }
        }
    }
}
