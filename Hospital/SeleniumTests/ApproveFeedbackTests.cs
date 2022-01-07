using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using SeleniumTests.Pages;
using Xunit;

namespace SeleniumTests
{
    public class ApproveFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private ApproveFeedbackPage approveFeedbackPage;
        public readonly string LoginUri = "http://localhost:4200/login";
        private LoginPage loginPage;

        public ApproveFeedbackTests() {
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");            
            options.AddArguments("disable-infobars");           
            options.AddArguments("--disable-extensions");       
            options.AddArguments("--disable-gpu");              
            options.AddArguments("--disable-dev-shm-usage");    
            options.AddArguments("--no-sandbox");               
            options.AddArguments("--disable-notifications");    
            driver = new ChromeDriver(options);
            loginPage = new LoginPage(driver,LoginUri);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForAdminIsDisplayed();
            approveFeedbackPage = new ApproveFeedbackPage(driver);
        }
        [Fact]
        public void ApproveFeedback()
        {
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            approveFeedbackPage.Navigate();
            approveFeedbackPage.EnsurePageIsDisplayed();
            approveFeedbackPage.Approve();
            Assert.True(approveFeedbackPage.UnapproveButtonDisplayed());
            Assert.True(approveFeedbackPage.IsSnackBarDisplayed());
            Assert.Equal(driver.Url, approveFeedbackPage.URI);
        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("admin");
            loginPage.InsertPassword("Admin123.");
            loginPage.Submit();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
