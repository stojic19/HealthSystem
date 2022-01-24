using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Net.Mime;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using Xunit;

namespace SeleniumTests
{
    [Collection("Sequence")]
    public class ApproveFeedbackTests : BaseTest, IDisposable
    {
        private readonly IWebDriver driver;
        private ApproveFeedbackPage approveFeedbackPage;
        private LoginPage loginPage;

        public ApproveFeedbackTests(BaseFixture fixture) : base(fixture)
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");
            driver = new ChromeDriver(Environment.CurrentDirectory, options);
            loginPage = new LoginPage(driver);
            loginPage.NavigateMan();
            loginPage.EnsureLoginFormForUserIsDisplayed();
            approveFeedbackPage = new ApproveFeedbackPage(driver);
        }
        [Fact]
        public void ApproveFeedback()
        {
            RegisterUser("Manager");
            ArrangeDatabase();
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            approveFeedbackPage.Navigate();
            approveFeedbackPage.EnsurePageIsDisplayed();
            approveFeedbackPage.Approve();
            Assert.True(approveFeedbackPage.UnapproveButtonDisplayed());
            Assert.True(approveFeedbackPage.IsSnackBarDisplayed());
            Assert.Equal(driver.Url, approveFeedbackPage.GetUrl());
            ClearDatabase();
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
            driver.Quit();
            driver.Dispose();
        }

        private void ArrangeDatabase()
        {

            RegisterUser("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

            var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetAll()
                    .FirstOrDefault(x => x.Patient.UserName == "testPatientUsername");
            if (feedback != null) return;
            
            feedback = new Feedback(patient.Id, "Osoblje je ljubazno.Sve pohvale.", true, false);
            UoW.GetRepository<IFeedbackWriteRepository>().Add(feedback);
        }

        private void ClearDatabase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
               .FirstOrDefault(x => x.UserName == "testPatientUsername");
            if (patient == null) return;

            {
                var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetAll()
                    .FirstOrDefault(x => x.PatientId == patient.Id);
                if (feedback != null) UoW.GetRepository<IFeedbackWriteRepository>().Delete(feedback);
            }
        }
    }
}
