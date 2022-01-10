using IntegrationEndToEndTests.Base;
using IntegrationEndToEndTests.Pages;
using Integration.Tendering.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace IntegrationEndToEndTests
{
    public class AddTenderTests : BaseTest
    {
        private readonly AddTenderPage _addTenderPage;
        private readonly LoginPage _loginPage;

        public AddTenderTests(BaseFixture fixture) : base(fixture)
        {
            _addTenderPage = new AddTenderPage(_driver);
            _loginPage = new LoginPage(_driver);
        }
        
        [Fact]
        public void Error_tender_name_invalid()
        {
            Login();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            _addTenderPage.CreateTender();
            Assert.Contains(_addTenderPage.GetToastrMessage(), AddTenderPage.InvalidTenderNameMessage);
        }
        [Fact]
        public void Error_medicines_table_invalid()
        {
            Login();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            _addTenderPage.InsertTenderName("Tender");
            _addTenderPage.CreateTender();
            Assert.Contains(_addTenderPage.GetToastrMessage(), AddTenderPage.InvalidMedicineLengthMessage);
        }
        [Fact]
        public void Error_add_medicine_name_invalid()
        {
            Login();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            _addTenderPage.AddMedicine();
            Assert.Contains(_addTenderPage.GetToastrMessage(), AddTenderPage.InvalidMedicineNameMessage);
        }
        [Fact]
        public void Error_add_medicine_quantity_invalid()
        {
            Login();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            _addTenderPage.InsertMedicineName("Brufen");
            _addTenderPage.AddMedicine();
            Assert.Contains(_addTenderPage.GetToastrMessage(), AddTenderPage.InvalidMedicineQuantityMessage);
        }
        [Fact]
        public void Error_add_medicine_name_not_unique()
        {
            Login();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            AddMedicine("Brufen", 100);
            AddMedicine("Brufen", 100);
            Assert.Contains(_addTenderPage.GetToastrMessage(), AddTenderPage.InvalidMedicineNameNotUniqueMessage);
        }
        [Fact]
        public void Error_end_date_before_start_date()
        {
            Login();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            _addTenderPage.InsertTenderName("Tender");
            _addTenderPage.InsertEndDate(DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy"));
            AddMedicine("Brufen", 100);
            _addTenderPage.CreateTender();
            Thread.Sleep(2000);
            Assert.Contains(_addTenderPage.GetToastrMessage(), AddTenderPage.InvalidDateMessage);
        }
        [Fact]
        public void Success_Tender_Creation()
        {
            Login();
            var beforeTest = UoW.GetRepository<ITenderReadRepository>().GetAll().ToList();
            _addTenderPage.Navigate();
            _addTenderPage.WaitForDisplay();
            _addTenderPage.InsertTenderName("Tender");
            AddMedicine("Brufen", 100);
            _addTenderPage.CreateTender();
            Thread.Sleep(2000);
            var afterTest = UoW.GetRepository<ITenderReadRepository>().GetAll().ToList();
            int difference = afterTest.Count - beforeTest.Count;
            foreach (var tender_after in afterTest)
            {
                bool existed = false;
                foreach (var tender_before in beforeTest)
                {
                    if (tender_after.Id == tender_before.Id)
                    {
                        existed = true;
                    }
                }
                if (!existed)
                {
                    UoW.GetRepository<ITenderWriteRepository>().Delete(tender_after);
                    break;
                }
            }
            difference.ShouldBe(1);
        }
        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Stojic19");
            _loginPage.InsertPassword("Stojic19");
            _loginPage.Submit();
        }

        private void AddMedicine(string name, int quantity)
        {
            _addTenderPage.InsertMedicineName(name);
            _addTenderPage.InsertMedicineQuantity(quantity);
            _addTenderPage.AddMedicine();
        }
    }
}
