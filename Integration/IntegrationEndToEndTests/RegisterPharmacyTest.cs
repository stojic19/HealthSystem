using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integration.Pharmacies.Repository;
using IntegrationEndToEndTests.Base;
using IntegrationEndToEndTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace IntegrationEndToEndTests
{
    public class RegisterPharmacyTest : BaseTest, IDisposable
    {
        private PharmacyRegistrationPage registrationPage;
        private LoginPage loginPage;

        public RegisterPharmacyTest(BaseFixture fixture) : base(fixture)
        {
            registrationPage = new PharmacyRegistrationPage(_driver);
            loginPage = new LoginPage(_driver);
        }

        [Fact]
        public void Register_success()
        {
            loginPage.Navigate();
            loginPage.InsertUsername("Rade");
            loginPage.InsertPassword("RadeRade654#@!");
            loginPage.Submit();
            var beforeTest = UoW.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            registrationPage.Navigate();
            registrationPage.WaitForDisplay();
            registrationPage.InsertName("Apoteka");
            registrationPage.InsertCityName("Novi Sad");
            registrationPage.InsertCountry("Srbija");
            registrationPage.InsertPostalCode("21000");
            registrationPage.InsertBaseUrl("https://localhost:44304");
            registrationPage.InsertStreetName("Vojvode Stepe");
            registrationPage.InsertStreetNumber("14");
            registrationPage.Submit();
            Thread.Sleep(3000);
            var afterTest = UoW.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            int difference = afterTest.Count() - beforeTest.Count();
            foreach (var pharmacy1 in afterTest)
            {
                bool existed = false;
                foreach (var pharmacy2 in beforeTest)
                {
                    if (pharmacy1.Id == pharmacy2.Id) existed = true;
                }

                if (!existed)
                {
                    UoW.GetRepository<IPharmacyWriteRepository>().Delete(pharmacy1);
                    break;
                }
            }
            Assert.True(difference == 1);
        }
    }
}
