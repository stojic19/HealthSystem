using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using Microsoft.EntityFrameworkCore;
using SeleniumTests.Base;
using SeleniumTests.Pages;
using Xunit;

namespace SeleniumTests
{
    [Collection("Sequence")]
    public class ApproveFeedbackTests : BaseTest,IDisposable
    {
        private readonly IWebDriver driver;
        private ApproveFeedbackPage approveFeedbackPage;
        private LoginPage loginPage;

        public ApproveFeedbackTests(BaseFixture fixture) : base(fixture) {
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
            loginPage.EnsureLoginFormForAdminIsDisplayed();
            approveFeedbackPage = new ApproveFeedbackPage(driver);
        }
        [Fact]
        public void ApproveFeedback()
        {
            RegisterUser("Manager");
            ArrangeDatabase();
            InsertCredentials();
            loginPage.EnsureAdminIsLoggedIn();
            approveFeedbackPage.Navigate();
            approveFeedbackPage.EnsurePageIsDisplayed();
            approveFeedbackPage.Approve();
            Assert.True(approveFeedbackPage.UnapproveButtonDisplayed());
            Assert.True(approveFeedbackPage.IsSnackBarDisplayed());
            Assert.Equal(driver.Url, approveFeedbackPage.GetUrl());
            ClearDatabase();
            DeleteDataFromDataBase();
        }

        private void InsertCredentials()
        {
            loginPage.InsertUsername("testLogInManager");
            loginPage.InsertPassword("111111aA");
            loginPage.Submit();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        private void ArrangeDatabase()
        {

            RegisterUser("Patient");
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(p => p.UserName == "testPatientUsername");

            var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetAll()
                    .FirstOrDefault(x => x.Patient.Id == patient.Id);
            if (feedback != null) return;
            feedback = new Feedback()
                {
                    Patient = patient,
                    Text = "Osoblje je ljubazno.Sve pohvale.",
                    CreatedDate = new DateTime(2021,12,12,12,30,0),
                    IsPublishable = true
                };
                UoW.GetRepository<IFeedbackWriteRepository>().Add(feedback);
        }

        private void ClearDatabase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
               .FirstOrDefault(x => x.UserName == "TestUsername");
            if (patient == null) return;

            {
                var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetAll()
                    .FirstOrDefault(x => x.Patient == patient);
                if (feedback != null) UoW.GetRepository<IFeedbackWriteRepository>().Delete(feedback);
            }
        }
    }
}
