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
using Shouldly;
using Xunit;

namespace IntegrationEndToEndTests
{
    public class RegisterPharmacyTest : BaseTest
    {
        private readonly PharmacyRegistrationPage _registrationPage;
        private readonly LoginPage _loginPage;

        public RegisterPharmacyTest(BaseFixture fixture) : base(fixture)
        {
            _registrationPage = new PharmacyRegistrationPage(_driver);
            _loginPage = new LoginPage(_driver);
        }

        [Fact]
        public void Register_success()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Rade");
            _loginPage.InsertPassword("RadeRade654#@!");
            _loginPage.Submit();
            var beforeTest = UoW.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            _registrationPage.Navigate();
            _registrationPage.WaitForDisplay();
            _registrationPage.InsertName("Apoteka");
            _registrationPage.InsertCityName("Novi Sad");
            _registrationPage.InsertCountry("Srbija");
            _registrationPage.InsertPostalCode("21000");
            _registrationPage.InsertBaseUrl("https://localhost:44304");
            _registrationPage.InsertStreetName("Vojvode Stepe");
            _registrationPage.InsertStreetNumber("14");
            _registrationPage.Submit();
            Thread.Sleep(5000);
            var afterTest = UoW.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            int difference = afterTest.Count - beforeTest.Count;
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
            difference.ShouldBe(1);
        }
    }
}
