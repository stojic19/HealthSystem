using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTests
{
    [Collection("Sequence")]
    public class CancelingRenovationTest : BaseTest, IDisposable
    {
        private readonly IWebDriver driver;
        private readonly CancelRenovationPage cancelRenovationPage;
        private readonly LoginPage loginPage;

        public CancelingRenovationTest(BaseFixture fixture) : base(fixture)
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
            cancelRenovationPage = new CancelRenovationPage(driver);
        }

        [Fact]
        public void CancelRenovation()
        { 
            RegisterUser("Manager");

            var events = ArrangeDatabase();
            var testRoom = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .Where(x => x.Name == "TestCancelRoom")
                .FirstOrDefault();

            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            cancelRenovationPage.Navigate(testRoom.Id);
            cancelRenovationPage.EnsurePageIsDisplayed();
            var initialRowsCount = cancelRenovationPage.EventsCount();
            cancelRenovationPage.CancelEvent();
            Assert.True(cancelRenovationPage.EventsNumberChanged(initialRowsCount));
            Assert.Equal(driver.Url, "http://localhost:4200/schedule/" + testRoom.Id);
            ClearDatabase();
            DeleteDataFromDataBase();
        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("testLogInManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }

        private RoomRenovationEvent ArrangeDatabase()
        {
            var testRoom = UoW.GetRepository<IRoomReadRepository>().GetAll().Where(x => x.Name == "TestCancelRoom").FirstOrDefault();
            
            if (testRoom == null)
            {
                testRoom = new Room()
                {
                    Name = "TestCancelRoom",
                    FloorNumber = 1,
                    BuildingName = "Building 1",
                    RoomType = RoomType.AppointmentRoom,
                    RoomPosition = new Hospital.GraphicalEditor.Model.RoomPosition(155, 155, 155, 155)
                };
                UoW.GetRepository<IRoomWriteRepository>().Add(testRoom);

            }

            RoomRenovationEvent scheduledEvent = new RoomRenovationEvent()
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                RoomId = testRoom.Id,
                IsMerge = false,
                FirstRoomType = RoomType.AppointmentRoom,
                FirstRoomDescription = "First split room",
                FirstRoomName = "First room",
                SecondRoomDescription = "Second split room",
                SecondRoomName = "Second room",
                SecondRoomType = RoomType.AppointmentRoom
            };

            UoW.GetRepository<IRoomRenovationEventWriteRepository>().Add(scheduledEvent);
            return scheduledEvent;
        }

        private void ClearDatabase()
        {
            Room room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .Where(x => x.Name == "TestCancelRoom")
                .FirstOrDefault();
            
            if (room != null)
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
