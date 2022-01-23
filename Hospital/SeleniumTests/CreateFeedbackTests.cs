using System;
using System.Linq;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Repository;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using Xunit;

namespace SeleniumTests
{
    [Collection("Sequence")]
    public class CreateFeedbackTests :  BaseTest, IDisposable
    {
        private readonly IWebDriver driver;
        private readonly Pages.CreateFeedbackPage _createFeedbackPage;
        public readonly LoginPage LoginPage;

        public CreateFeedbackTests(BaseFixture fixture) : base(fixture)
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless");
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications
            driver = new ChromeDriver(options);

            LoginPage = new LoginPage(driver);
            LoginPage.Navigate();
            LoginPage.EnsureLoginFormForUserIsDisplayed();
            _createFeedbackPage = new Pages.CreateFeedbackPage(driver);
            _createFeedbackPage.Navigate();
        }
        
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void NewFeedbackCreated()
        {
            RegisterUser("Patient");
            InsertCredentials();
            LoginPage.EnsureUserIsLogged();
            _createFeedbackPage.Navigate();
            _createFeedbackPage.OpenModal();
            _createFeedbackPage.WaitForDialogOpened();
            _createFeedbackPage.InsertFeedback("Best services in town.");
            _createFeedbackPage.InsertIsAnonymous(false);
            _createFeedbackPage.InsertIsPublishable(true);
            _createFeedbackPage.SubmitForm();
            _createFeedbackPage.WaitForFormSubmitted();
            Assert.True(_createFeedbackPage.IsSnackbarDisplayed());
            Assert.Equal(_createFeedbackPage._uri, driver.Url);
            DeleteInsertedFeedback();
            DeleteDataFromDataBase();
        }

        [Fact]
        public void FeedbackSubmitFailed()
        {
            RegisterUser("Patient");
            InsertCredentials();
            LoginPage.EnsureUserIsLogged();
            _createFeedbackPage.Navigate();
            _createFeedbackPage.OpenModal();
            _createFeedbackPage.WaitForDialogOpened();
            _createFeedbackPage.InsertIsAnonymous(false);
            _createFeedbackPage.InsertIsPublishable(true);
            _createFeedbackPage.SubmitForm();
            Assert.False(_createFeedbackPage.IsSnackbarDisplayed());
            Assert.Equal(_createFeedbackPage._uri, driver.Url);
            DeleteInsertedFeedback();
            DeleteDataFromDataBase();
        }

        private void InsertCredentials()
        {
            LoginPage.InsertUsername("testPatientUsername");
            LoginPage.InsertPassword("TestProba123");
            LoginPage.Submit();
        }

        private void DeleteInsertedFeedback()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

            var insertedFeedback = UoW.GetRepository<IFeedbackReadRepository>().GetAll()
                .FirstOrDefault(f => f.Patient.Id == patient.Id);

            if (insertedFeedback != null) UoW.GetRepository<IFeedbackWriteRepository>().Delete(insertedFeedback); 
        }

    }
}
