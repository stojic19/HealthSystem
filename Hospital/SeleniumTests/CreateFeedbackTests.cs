using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using Xunit;

namespace SeleniumTests
{
    public class CreateFeedbackTests :  BaseTest
    {
        private readonly IWebDriver _driver;
        private readonly Pages.CreateFeedbackPage _createFeedbackPage;
        public readonly LoginPage LoginPage;

        public CreateFeedbackTests(BaseFixture fixture) : base(fixture)
        {
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications
            _driver = new ChromeDriver(options);

            LoginPage = new LoginPage(_driver);
            LoginPage.Navigate();
            LoginPage.EnsureLoginFormForUserIsDisplayed();
            _createFeedbackPage = new Pages.CreateFeedbackPage(_driver);
            _createFeedbackPage.Navigate();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void NewFeedbackCreated()
        {
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
            Assert.Equal(_createFeedbackPage._uri, _driver.Url);
        }

        [Fact]
        public void FeedbackSubmitFailed()
        {
            InsertCredentials();
            LoginPage.EnsureUserIsLogged();
            _createFeedbackPage.Navigate();
            _createFeedbackPage.OpenModal();
            _createFeedbackPage.WaitForDialogOpened();
            _createFeedbackPage.InsertIsAnonymous(false);
            _createFeedbackPage.InsertIsPublishable(true);
            _createFeedbackPage.SubmitForm();
            Assert.False(_createFeedbackPage.IsSnackbarDisplayed());
            Assert.Equal(_createFeedbackPage._uri, _driver.Url);
        }

        private void InsertCredentials()
        {
            LoginPage.InsertUsername("markgut1");
            LoginPage.InsertPassword("markGut1");
            LoginPage.Submit();
        }
    }
}