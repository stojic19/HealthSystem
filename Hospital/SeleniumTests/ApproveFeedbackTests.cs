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

        public ApproveFeedbackTests() {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            
            options.AddArguments("disable-infobars");           
            options.AddArguments("--disable-extensions");       
            options.AddArguments("--disable-gpu");              
            options.AddArguments("--disable-dev-shm-usage");    
            options.AddArguments("--no-sandbox");               
            options.AddArguments("--disable-notifications");    
            driver = new ChromeDriver(options);
            approveFeedbackPage = new Pages.ApproveFeedbackPage(driver);      
            approveFeedbackPage.Navigate();
            approveFeedbackPage.EnsurePageIsDisplayed();
        }
        [Fact]
        public void ApproveFeedback()
        {
            approveFeedbackPage.Approve();
            Assert.True(approveFeedbackPage.UnapproveButtonDisplayed());
            Assert.True(approveFeedbackPage.IsSnackbarDisplayed());
            Assert.Equal(driver.Url, approveFeedbackPage.URI);
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
