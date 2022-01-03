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
        private PharmacyRegistrationPage page;

        public RegisterPharmacyTest(BaseFixture fixture) : base(fixture)
        {
            page = new PharmacyRegistrationPage(_driver);
            page.Navigate();
            page.WaitForDisplay();
        }

        [Fact]
        public void Register_success()
        {
            var beforeTest = UoW.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            page.InsertName("Apoteka");
            page.InsertCityName("Novi Sad");
            page.InsertCountry("Srbija");
            page.InsertPostalCode("21000");
            page.InsertBaseUrl("https://localhost:44304");
            page.InsertStreetName("Vojvode Stepe");
            page.InsertStreetNumber("14");
            page.Submit();
            Thread.Sleep(2000);
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
