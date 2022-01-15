using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Linq;
using Xunit;

namespace SeleniumTests
{
    [Collection("Sequence")]
    public class BlockMaliciousPatientsTest : BaseTest, IDisposable
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
            loginPage.EnsureLoginFormForAdminIsDisplayed();
            _blockMaliciousPatientsPage = new Pages.BlockMaliciousPatientsPage(_driver);
        }

        [Fact]
        public void BlockPatient()
        {
            RegisterUser("Manager");
            ArrangeDatabase();
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            _blockMaliciousPatientsPage.Navigate();
            _blockMaliciousPatientsPage.EnsurePageIsDisplayed();
            patientCount = _blockMaliciousPatientsPage.PatientCount();
            _blockMaliciousPatientsPage.BlockPatient();
            Assert.True(_blockMaliciousPatientsPage.IsSnackBarDisplayed());
            Assert.Equal(_driver.Url, _blockMaliciousPatientsPage.URI);
            //Assert.Equal(patientCount - 1, _.PatientCount());
            DeleteDataFromDataBase();
        }
        private void InsertCredentials()
        {
            loginPage.InsertUsername("testLogInManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
        private void ArrangeDatabase()
        {
            RegisterUser("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll().Include(p => p.MedicalRecord).ThenInclude(mr => mr.Doctor)
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == patient.MedicalRecord.DoctorId);
                if (scheduledEvent != null) return;
                scheduledEvent = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(-100),
                    EndDate = DateTime.Now.AddDays(-100),
                    CancellationDate = DateTime.Now.AddDays(-110),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);

                var scheduledEvent2 = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(-22),
                    EndDate = DateTime.Now.AddDays(-22),
                    CancellationDate = DateTime.Now.AddDays(-27),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
                var scheduledEvent3 = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(-4),
                    EndDate = DateTime.Now.AddDays(-4),
                    CancellationDate = DateTime.Now.AddDays(-14),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent3);
                var scheduledEvent4 = new ScheduledEvent()
                {
                    StartDate = DateTime.Now.AddDays(7),
                    EndDate = DateTime.Now.AddDays(7),
                    CancellationDate = DateTime.Now.AddDays(-1),
                    Doctor = patient.MedicalRecord.Doctor,
                    IsCanceled = true,
                    IsDone = false,
                    Patient = patient,
                    Room = patient.MedicalRecord.Doctor.Room,
                    ScheduledEventType = ScheduledEventType.Appointment
                };
                UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent4);
        }
    }
}
