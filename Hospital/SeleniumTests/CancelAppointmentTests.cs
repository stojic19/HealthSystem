using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
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
    public class CancelAppointmentTests : BaseTest, IDisposable
    {

        private readonly IWebDriver driver;
        private readonly CancelAppointmentPage cancelAppointmentPage;
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
            RegisterUser("Patient");
            var events = ArrangeDatabase(false, false);
            InsertCredentials();
            loginPage.EnsureUserIsLogged();
            cancelAppointmentPage.EnsurePageIsDisplayed();
            cancelAppointmentPage.CancelAppointment();
            Assert.True(cancelAppointmentPage.ElementNumberChanged());
            Assert.Equal(driver.Url, cancelAppointmentPage.URI);
            ClearDatabase(events);
            DeleteDataFromDataBase();
        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("testPatientUsername");
            loginPage.InsertPassword("TestProba123");
            loginPage.Submit();
        }

        private ScheduledEvent ArrangeDatabase(bool isCanceled, bool isDone)
        {

            var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Room).FirstOrDefault(x => x.UserName == "testDoctorUsername");
            var testPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.UserName == "testPatientUsername").FirstOrDefault();

            ScheduledEvent scheduledEvent = new ScheduledEvent(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);
            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
            return scheduledEvent;
        }

        private void ClearDatabase(ScheduledEvent events)
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
