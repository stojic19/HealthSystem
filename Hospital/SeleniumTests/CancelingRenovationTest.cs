using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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

            ArrangeDatabase();
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

            Assert.True(cancelRenovationPage.EventsNumberChanged());
            Assert.Equal(driver.Url, cancelRenovationPage.URI + testRoom.Id);

            DeleteDataFromDataBase();
        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("testLogInManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }

        private void ArrangeDatabase()
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
                };
                UoW.GetRepository<IRoomWriteRepository>().Add(testRoom);

            }

            RoomRenovationEvent renovation = new RoomRenovationEvent()
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(4).Day),
                RoomId = testRoom.Id,
                IsMerge = false,
                FirstRoomType = RoomType.AppointmentRoom,
                FirstRoomDescription = "First split room",
                FirstRoomName = "First room",
                SecondRoomDescription = "Second split room",
                SecondRoomName = "Second room",
                SecondRoomType = RoomType.AppointmentRoom
            };

            UoW.GetRepository<IRoomRenovationEventWriteRepository>().Add(renovation);
        }

        private void ClearDatabase()
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .Where(x => x.Name == "TestCancelRoom").FirstOrDefault();

            if (room != null)
            {
                DeleteRoom(room);
            }

        }

        private void DeleteRoom(Room room)
        {
            var polingInterval = 10;
            var timeout = 1500;

            while (timeout != 0)
            {
                timeout -= polingInterval;

                try
                {
                    UoW.GetRepository<IRoomWriteRepository>().Delete(room, true);
                    return;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Thread.Sleep(polingInterval);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            throw new Exception("Deleting unsuccessful");
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
