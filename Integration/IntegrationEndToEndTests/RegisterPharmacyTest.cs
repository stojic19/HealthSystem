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
        private readonly PharmacyListPage _pharmaciesPage;

        public RegisterPharmacyTest(BaseFixture fixture) : base(fixture)
        {
            _registrationPage = new PharmacyRegistrationPage(_driver);
            _loginPage = new LoginPage(_driver);
            _pharmaciesPage = new PharmacyListPage(_driver);
        }

        [Fact]
        public void Register_success()
        {
            Login();
            var beforeTest = UoW.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            _pharmaciesPage.Navigate();
            _pharmaciesPage.WaitForDisplay();
            int countBeforeRegistration = _pharmaciesPage.PharmaciesCount();
            _registrationPage.Navigate();
            _registrationPage.WaitForDisplay();
            _registrationPage.InsertBaseUrl("http://localhost:44304");
            _registrationPage.Submit();
            Thread.Sleep(5000);
            _pharmaciesPage.Navigate();
            _pharmaciesPage.WaitForDisplay();
            int countAfterRegistration = _pharmaciesPage.PharmaciesCount();
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
            (countAfterRegistration - countBeforeRegistration).ShouldBe(1);
        }

        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.WaitForDisplay();
            _loginPage.InsertUsername("Rade");
            _loginPage.InsertPassword("RadeRade654#@!");
            _loginPage.Submit();
        }

        [Fact]
        public void Register_empty_url()
        {
            Login();
            Thread.Sleep(250);
            _registrationPage.Navigate();
            _registrationPage.WaitForDisplay();
            _registrationPage.Submit();
            Thread.Sleep(250);
            Assert.Contains(_registrationPage.GetToastrMessage(), _registrationPage.EmptyUrl);
        }
        [Fact]
        public void Register_bad_url()
        {
            Login();
            Thread.Sleep(250);
            _registrationPage.Navigate();
            _registrationPage.WaitForDisplay();
            _registrationPage.InsertBaseUrl("https://localhost:4430");
            _registrationPage.Submit();
            Thread.Sleep(6000);
            Assert.Contains(_registrationPage.GetToastrMessage(), _registrationPage.InvalidUrl);
        }
    }
}
