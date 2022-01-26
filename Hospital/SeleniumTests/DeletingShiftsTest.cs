using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTests
{
    public class DeletingShiftsTest : BaseTest, IDisposable
    {
        private readonly IWebDriver driver;
        private readonly DeletingShiftsPage deleteShiftsPage;
        private readonly LoginPage loginPage;
        public DeletingShiftsTest(BaseFixture fixture) : base(fixture)
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
            deleteShiftsPage = new DeletingShiftsPage(driver);
        }

        [Fact]
        public void DeleteShift()
        {
            RegisterUser("Manager");

            var events = ArrangeDatabase();
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            deleteShiftsPage.Navigate();
            deleteShiftsPage.EnsurePageIsDisplayed();
            deleteShiftsPage.DeleteShift();

            Assert.True(deleteShiftsPage.EventsNumberChanged());
            Assert.Equal(driver.Url, deleteShiftsPage.URI);

            DeleteDataFromDataBase();
            ClearDataBase();
        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("testLogInManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }

        private Shift ArrangeDatabase()
        {
            var testShift = UoW.GetRepository<IShiftReadRepository>()
                .GetAll()
                .Where(x => x.Name == "Test delete shift")
                .FirstOrDefault();

            if (testShift == null)
            {
                testShift = new Shift
                {
                    Name = "Test delete shift",
                    From = 7,
                    To = 14
                };

                UoW.GetRepository<IShiftWriteRepository>().Add(testShift);
            }

            return testShift;
        }

        private void ClearDataBase()
        {
            var testShift = UoW.GetRepository<IShiftReadRepository>()
                .GetAll()
                .Where(x => x.Name == "Test delete shift")
                .FirstOrDefault();

            if (testShift != null)
                UoW.GetRepository<IShiftWriteRepository>().Delete(testShift);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
