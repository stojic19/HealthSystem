using AutoMapper;
using AutoMapper.Configuration;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTests
{
    public class BlockMaliciousPatientsTest : BaseTest
    {
        private readonly IWebDriver _driver;
        private readonly Pages.BlockMaliciousPatientsPage _blockMaliciousPatientsPage;
        private LoginPage loginPage;
        private int patientCount = 0;

        public BlockMaliciousPatientsTest(BaseFixture fixture) : base(fixture)
        {
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");
            _driver = new ChromeDriver(options);
            loginPage = new LoginPage(_driver);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForUserIsDisplayed();
            _blockMaliciousPatientsPage = new Pages.BlockMaliciousPatientsPage(_driver);
        }

        [Fact]
        public async Task BlockPatientAsync()
        {
            await ArrangeDatabaseAsync();
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            _blockMaliciousPatientsPage.Navigate();
            _blockMaliciousPatientsPage.EnsurePageIsDisplayed();
            patientCount = _blockMaliciousPatientsPage.PatientCount();
            _blockMaliciousPatientsPage.BlockPatient();
            Assert.True(_blockMaliciousPatientsPage.IsSnackBarDisplayed());
            Assert.Equal(_driver.Url, _blockMaliciousPatientsPage.URI);
            //Assert.Equal(patientCount - 1, _.PatientCount());
            ClearDatabase();
        }
        private void InsertCredentials()
        {
            loginPage.InsertUsername("testManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
        private async Task ArrangeDatabaseAsync()
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
                    Shift = new Shift()
                    {
                        From = 8,
                        To = 4,
                        Name = "prva"
                    },
                    Room = room,
                    City = city

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername");

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
                    UserName = "testUsername",
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

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == doctor.Id);
                if (scheduledEvent == null)
                {
                    scheduledEvent = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-100),
                        EndDate = DateTime.Now.AddDays(-100),
                        CancellationDate = DateTime.Now.AddDays(-110),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);

                    var scheduledEvent2 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-22),
                        EndDate = DateTime.Now.AddDays(-22),
                        CancellationDate = DateTime.Now.AddDays(-27),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
                    var scheduledEvent3 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-4),
                        EndDate = DateTime.Now.AddDays(-4),
                        CancellationDate = DateTime.Now.AddDays(-14),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent3);
                    var scheduledEvent4 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(7),
                        EndDate = DateTime.Now.AddDays(7),
                        CancellationDate = DateTime.Now.AddDays(-1),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent4);
                }

                
            }
            var managerReadRepo = UoW.GetRepository<IManagerReadRepository>();
            var manager = managerReadRepo.GetAll().Where(x => x.UserName.Equals("testManager")).FirstOrDefault();
            if (manager == null)
            {

                await RegisterManagerAsync(city.Id);
                manager = managerReadRepo.GetAll().Where(x => x.UserName.Equals("testManager")).FirstOrDefault();
            }

        }

        private async Task RegisterManagerAsync(int cityId)
        {
            var managerDto = new NewManagerDTO()
            {
                FirstName = "TestManager",
                LastName = "TestManagerLastName",
                MiddleName = "TestMnagerMiddleName",
                DateOfBirth =  new DateTime(),
                Gender = Gender.Female,
                Street = "TestManagerStreet",
                UserName = "testManager",
                Email = "testManager@gmail.com",
                CityId = cityId,
                Password = "111111aA"
            };
            var content = GetContent(managerDto);
            var response = await Client.PostAsync(BaseUrl + "api/Registration/RegisterManager", content);
        }

        private void ClearDatabase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
               .FirstOrDefault(x => x.UserName == "testUsername");
            if (patient == null) return;

            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll().Include(mr => mr.Doctor)
                    .FirstOrDefault(x => x.Id == patient.MedicalRecordId);

                UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
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
            var shift = UoW.GetRepository<IShiftReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "prva");

            if (shift != null)
            {
                UoW.GetRepository<IShiftWriteRepository>().Delete(shift);
            }
        }
    }
}
