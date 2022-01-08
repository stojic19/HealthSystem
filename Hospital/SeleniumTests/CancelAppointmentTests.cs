using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;
using System;
using Xunit;

namespace SeleniumTests
{
    public class CancelAppointmentTests : IDisposable
    {

        private readonly IWebDriver driver;
        private readonly CancelAppointmentPage cancelAppointmentPage;
        public readonly string LoginUri = "http://localhost:4200/record";
        private readonly LoginPage loginPage;

        public CancelAppointmentTests()
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
            loginPage = new LoginPage(driver, LoginUri);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForUserIsDisplayed();
            cancelAppointmentPage = new CancelAppointmentPage(driver);   
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void CancelAppointment()
        {
            InsertCredentials();
            loginPage.EnsureUserIsLogged();
            cancelAppointmentPage.EnsurePageIsDisplayed();
            cancelAppointmentPage.CancelAppointment();
            Assert.True(cancelAppointmentPage.ElementNumberChanged());
            

            //Assert.Equal(driver.Url, cancelAppointmentPage.URI);

        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("andji");
            loginPage.InsertPassword("Andji1234");
            loginPage.Submit();
        }
    }
}
