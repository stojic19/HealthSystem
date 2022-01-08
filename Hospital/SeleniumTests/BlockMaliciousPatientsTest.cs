using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTests
{
    public class BlockMaliciousPatientsTest : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly Pages.BlockMaliciousPatientsPage _blockMaliciousPatientsPage;
        public readonly string LoginUri = "http://localhost:4200/login";
        private LoginPage loginPage;
        
        public BlockMaliciousPatientsTest()
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
            loginPage = new LoginPage(_driver, LoginUri);
            loginPage.Navigate();
            loginPage.EnsureLoginFormForAdminIsDisplayed();
            _blockMaliciousPatientsPage = new Pages.BlockMaliciousPatientsPage(_driver);
        }

        [Fact]
        public void BlockPatient()
        {
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            _blockMaliciousPatientsPage.Navigate();
            _blockMaliciousPatientsPage.EnsurePageIsDisplayed();
            var patientCount = _blockMaliciousPatientsPage.PatientCount();
            _blockMaliciousPatientsPage.BlockPatient();
            Assert.True(_blockMaliciousPatientsPage.IsSnackBarDisplayed());
            Assert.Equal(_driver.Url, _blockMaliciousPatientsPage.URI);
            Assert.Equal(patientCount - 1, _blockMaliciousPatientsPage.PatientCount());
        }
        private void InsertCredentials()
        {
            loginPage.InsertUsername("manager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
