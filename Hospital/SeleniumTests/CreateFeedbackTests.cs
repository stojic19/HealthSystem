using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace SeleniumTests
{
    public class CreateFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly Pages.CreateFeedbackPage createFeedbackPage;

        public CreateFeedbackTests()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications
            driver = new ChromeDriver(options);

            createFeedbackPage = new Pages.CreateFeedbackPage(driver);
            createFeedbackPage.Navigate();
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void NewFeedbackCreated()
        {
            createFeedbackPage.OpenModal();
            createFeedbackPage.WaitForDialogOpened();
            createFeedbackPage.InsertFeedback("Best services in town.");
            createFeedbackPage.InsertIsAnonymous(false);
            createFeedbackPage.InsertIsPublishable(true);
            createFeedbackPage.SubmitForm();
            Assert.False(createFeedbackPage.IsFormElementDisplayed());
         }

        [Fact]
        public void FeedbackSubmitFailed()
        {
            createFeedbackPage.OpenModal();
            createFeedbackPage.WaitForDialogOpened();
            createFeedbackPage.InsertIsAnonymous(false);
            createFeedbackPage.InsertIsPublishable(true);
            createFeedbackPage.SubmitForm();
            Assert.True(createFeedbackPage.IsFormElementDisplayed());
         }
    }
}
