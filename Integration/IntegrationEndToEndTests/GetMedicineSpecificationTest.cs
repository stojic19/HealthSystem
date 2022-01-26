using IntegrationEndToEndTests.Base;
using IntegrationEndToEndTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Integration.Partnership.Repository;
using Integration.Pharmacies.Repository;
using Shouldly;
using Xunit;

namespace IntegrationEndToEndTests
{
    public class GetMedicineSpecificationTest : BaseTest
    {
        private readonly MedicineSpecificationListPage _medicineSpecificationListPage;
        private readonly LoginPage _loginPage;
        private readonly NewMedicineSpecificationPage _newMedicineSpecificationPage;

        public GetMedicineSpecificationTest(BaseFixture fixture) : base(fixture)
        {
            _medicineSpecificationListPage = new MedicineSpecificationListPage(_driver);
            _loginPage = new LoginPage(_driver);
            _newMedicineSpecificationPage = new NewMedicineSpecificationPage(_driver);
        }

        [Fact]
        public void Get_medicine_specification_success_aspirin()
        {
            Login();
            var beforeTest = UoW.GetRepository<IMedicineSpecificationFileReadRepository>().GetAll().ToList();
            _medicineSpecificationListPage.Navigate();
            _medicineSpecificationListPage.WaitForDisplay();
            int countBeforeTest = _medicineSpecificationListPage.MedicineSpecificationCount();
            _newMedicineSpecificationPage.Navigate();
            _newMedicineSpecificationPage.WaitForDisplay();
            _newMedicineSpecificationPage.InsertMedicineName("Aspirin");
            _newMedicineSpecificationPage.SelectFirstPharmacy();
            _newMedicineSpecificationPage.Submit();
            Thread.Sleep(5000);
            _medicineSpecificationListPage.Navigate();
            _medicineSpecificationListPage.WaitForDisplay();
            int countAfterTest = _medicineSpecificationListPage.MedicineSpecificationCount();
            var afterTest = UoW.GetRepository<IMedicineSpecificationFileReadRepository>().GetAll().ToList();
            int difference = afterTest.Count - beforeTest.Count;
            difference.ShouldBe(1);
            (countAfterTest - countBeforeTest).ShouldBe(1);
        }

        [Fact]
        public void Get_medicine_specification_empty_medicine_name()
        {
            Login();
            Thread.Sleep(250);
            _medicineSpecificationListPage.Navigate();
            _medicineSpecificationListPage.WaitForDisplay();
            _newMedicineSpecificationPage.Navigate();
            _newMedicineSpecificationPage.WaitForDisplay();
            _newMedicineSpecificationPage.SelectFirstPharmacy();
            _newMedicineSpecificationPage.Submit();
            Thread.Sleep(250);
            Assert.Contains(_newMedicineSpecificationPage.GetToastrMessage(), _newMedicineSpecificationPage.EmptyMedicineName);
        }
        [Fact]
        public void Get_medicine_specification_medicine_missing()
        {
            Login();
            Thread.Sleep(250);
            _medicineSpecificationListPage.Navigate();
            _medicineSpecificationListPage.WaitForDisplay();
            _newMedicineSpecificationPage.Navigate();
            _newMedicineSpecificationPage.WaitForDisplay();
            _newMedicineSpecificationPage.InsertMedicineName("Lek koji sigurno ne postoji");
            _newMedicineSpecificationPage.SelectFirstPharmacy();
            _newMedicineSpecificationPage.Submit();
            Thread.Sleep(2000);
            Assert.Contains(_newMedicineSpecificationPage.GetToastrMessage(), _newMedicineSpecificationPage.PharmacyOrMedicineMissing);
        }

        [Fact]
        public void Get_medicine_specification_closed_pharmacy()
        {
            Login();
            Thread.Sleep(250);
            _medicineSpecificationListPage.Navigate();
            _medicineSpecificationListPage.WaitForDisplay();
            _newMedicineSpecificationPage.Navigate();
            _newMedicineSpecificationPage.WaitForDisplay();
            _newMedicineSpecificationPage.InsertMedicineName("Aspirin");
            _newMedicineSpecificationPage.SelectSecondPharmacy();
            _newMedicineSpecificationPage.Submit();
            Thread.Sleep(2000);
            Assert.Contains(_newMedicineSpecificationPage.GetToastrMessage(), _newMedicineSpecificationPage.PharmacyOrMedicineMissing);
        }

        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.WaitForDisplay();
            _loginPage.InsertUsername("dunav");
            _loginPage.InsertPassword("ZutaBova2020");
            _loginPage.Submit();
        }
    }
}
