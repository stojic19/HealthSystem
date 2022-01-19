using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
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
            loginPage.NavigateMan();
            loginPage.EnsureLoginFormForUserIsDisplayed();
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
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Room)
                .FirstOrDefault(d => d.UserName == "testDoctorUsername");

            var date = new DateTime();
            var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == patient.MedicalRecord.DoctorId);
            if (scheduledEvent != null) return;
            scheduledEvent = new ScheduledEvent(0, true, false, DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-100),
                DateTime.Now.AddDays(-110), patient.Id, doctor.Id, doctor);

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);

            var scheduledEvent2 = new ScheduledEvent(0, true, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-22),
                DateTime.Now.AddDays(-27), patient.Id, doctor.Id, doctor);

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
            var scheduledEvent3 = new ScheduledEvent(0, true, false, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-4),
                DateTime.Now.AddDays(-14), patient.Id, doctor.Id, doctor);

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent3);
            var scheduledEvent4 = new ScheduledEvent(0, true, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7),
                DateTime.Now.AddDays(-1), patient.Id, doctor.Id, doctor);
            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent4);
        }
    }
}
