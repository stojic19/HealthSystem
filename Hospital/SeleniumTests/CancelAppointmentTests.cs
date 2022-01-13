using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using Microsoft.AspNetCore.Identity;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Linq;
using Xunit;

namespace SeleniumTests
{
    public class CancelAppointmentTests : BaseTest, IDisposable
    {

        private readonly IWebDriver driver;
        private readonly CancelAppointmentPage cancelAppointmentPage;
        public readonly string LoginUri = "http://localhost:4200/record";
        private readonly LoginPage loginPage;
     
        public CancelAppointmentTests(BaseFixture fixture) : base(fixture)
        {
           
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");

            driver = new ChromeDriver(options);
            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForUserIsDisplayed();
            cancelAppointmentPage = new CancelAppointmentPage(driver);   
        }

        [Fact]
        public void CancelAppointment()
        {

            InsertCredentials();
            loginPage.EnsureUserIsLogged();      
            cancelAppointmentPage.EnsurePageIsDisplayed();
            cancelAppointmentPage.CancelAppointment();           
            Assert.True(cancelAppointmentPage.ElementNumberChanged());
            Assert.Equal(driver.Url, cancelAppointmentPage.URI);


        }
        private void InsertCredentials()
        {
            loginPage.InsertUsername("andji");
            loginPage.InsertPassword("Andji1234");
            loginPage.Submit();
        }

        private ScheduledEvent AddDataToDatabase(bool isCanceled, bool isDone)
        {
            #region Data

            var countryWriteRepo = UoW.GetRepository<ICountryWriteRepository>();
            var cityWriteRepo = UoW.GetRepository<ICityWriteRepository>();
            var roomWriteRepo = UoW.GetRepository<IRoomWriteRepository>();
            var specializationWriteRepo = UoW.GetRepository<ISpecializationWriteRepository>();
            var doctorWiteRepo = UoW.GetRepository<IDoctorWriteRepository>();
            var patientWriteRepo = UoW.GetRepository<IPatientWriteRepository>();

            var testCountry = UoW.GetRepository<ICountryReadRepository>().GetAll().Where(x => x.Name == "TestCountry").FirstOrDefault();
            var testCity = UoW.GetRepository<ICityReadRepository>().GetAll().Where(x => x.Name == "TestCity").FirstOrDefault();
            var testRoom = UoW.GetRepository<IRoomReadRepository>().GetAll().Where(x => x.Name == "TestRoom").FirstOrDefault();
            var testSpecialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll().Where(x => x.Name == "TestSpecialization").FirstOrDefault();
            var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Where(x => x.FirstName == "TestDoctor").FirstOrDefault(); 
            var testPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.FirstName == "TestPatient").FirstOrDefault();

            if (testCountry == null)
            {
                testCountry = new Country()
                {
                    Name = "TestCountry"
                };
                countryWriteRepo.Add(testCountry);
            }

            if (testCity == null)
            {
                testCity = new City()
                {
                    Name = "TestCity",
                    Country = testCountry,
                    PostalCode = 00000
                };
                cityWriteRepo.Add(testCity);
            }
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
            if (testSpecialization == null)
            {
                testSpecialization = new Specialization()
                {
                    Description = "DescriptionSpecialization",
                    Name = "TestSpecialization"
                };
                specializationWriteRepo.Add(testSpecialization);
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
                    City = testCity,
                    Room = testRoom,
                    Shift = new Shift()
                    {
                        From = 8,
                        To = 4,
                        Name = "prva"
                    },
                    Specialization = testSpecialization,
                    UserName = "testDoctorUsername",
                    PasswordHash = "AQAAAAEAACcQAAAAEAUFvPAmYz/KKw+6GtOINkZCHX8uCRT8X5nX3/UBzPE1kIVqNXM+Efkye1IkDc6uIg==",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                   

                };
                doctorWiteRepo.Add(testDoctor);
            }
            if (testPatient == null)
            {
                MedicalRecord medicalRecord = new MedicalRecord
                {
                    Doctor = testDoctor,
                    Height = 0.0,
                    Weight = 0.0,
                    BloodType = BloodType.ABNegative,
                    JobStatus = JobStatus.Student

                };

                testPatient = new Patient()
                {
                    
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    City = testCity,
                    IsBlocked = false,
                    UserName = "testPatientUsername",
                    PasswordHash = "AQAAAAEAACcQAAAAEAUFvPAmYz/KKw+6GtOINkZCHX8uCRT8X5nX3/UBzPE1kIVqNXM+Efkye1IkDc6uIg==",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    MedicalRecord = medicalRecord,
                    NormalizedUserName = "TESTPATIENTUSERNAME",
                    SecurityStamp = "XJXBBWC54VRCAR5QA6Z6EGMKD4HIDH6M"
                };          
                patientWriteRepo.Add(testPatient);
            }
            #endregion

            ScheduledEvent scheduledEvent = new ScheduledEvent()
            {

                ScheduledEventType = 0,
                IsCanceled = isCanceled,
                IsDone = isDone,
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                Patient = testPatient,
                Doctor = testDoctor,
                Room = testRoom

            };

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
            return scheduledEvent;
        }

        private void DeleteDataFromDataBase(ScheduledEvent events)
        {
            UoW.GetRepository<IScheduledEventWriteRepository>().Delete(events, true);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
