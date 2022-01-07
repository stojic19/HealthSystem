using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace SeleniumTests
{
    public class ApproveFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.ApproveFeedbackPage approveFeedbackPage;
        private Pages.LoginPage loginPage;

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
            loginPage = new Pages.LoginPage(driver,loginPage.LoginUri);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForAdminIsDisplayed();
        }
        [Fact]
        public void ApproveFeedback()
        {
            InsertCredentials();
            if (!loginPage.IsSnackBarDisplayed()) return;
            approveFeedbackPage = new Pages.ApproveFeedbackPage(driver);
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
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
