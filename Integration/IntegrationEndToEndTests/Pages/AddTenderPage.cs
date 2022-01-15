using IntegrationEndToEndTests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;

namespace IntegrationEndToEndTests.Pages
{
    public class AddTenderPage : BasePage
    {
        private IWebElement TenderName => _driver.FindElement(By.Id("tenderName"));
        private IWebElement EndDate => _driver.FindElement(By.Id("endDate"));
        private IWebElement MedicineName => _driver.FindElement(By.Id("medicineName"));
        private IWebElement MedicineQuantity => _driver.FindElement(By.Id("medicineQuantity"));
        private IWebElement DatePickerButton => _driver.FindElement(By.Id("datePickerButton"));
        private IWebElement CreateTenderButton => _driver.FindElement(By.Id("createTenderButton"));
        private IWebElement AddMedicineButton => _driver.FindElement(By.Id("addMedicineButton"));

        public const string InvalidTenderNameMessage = "Enter valid tender name!";
        public const string InvalidMedicineLengthMessage = "You must add at least one medicine!";
        public const string InvalidMedicineNameMessage = "Enter valid medicine name!";
        public const string InvalidMedicineQuantityMessage = "Enter valid medicine quantity!";
        public const string InvalidMedicineNameNotUniqueMessage = "Enter unique medicine name";
        public const string InvalidDateMessage = "Start date must be before end date.";

        public AddTenderPage(IWebDriver driver) : base(driver) { }

        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition => CreateTenderButton.Displayed);
        }

        public void InsertTenderName(string tenderName)
        {
            this.TenderName.SendKeys(tenderName);
        }
        public void InsertEndDate(string endDate)
        {
            this.EndDate.SendKeys(endDate);
        }
        public void InsertMedicineName(string medicineName)
        {
            this.MedicineName.SendKeys(medicineName);
        }
        public void InsertMedicineQuantity(int medicineQuantity)
        {
            this.MedicineQuantity.SendKeys(medicineQuantity.ToString());
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_angularBaseUrl + "add-tender");

        public void CreateTender()
        {
            CreateTenderButton.Click();
        }
        public void ClickDatePicker()
        {
            DatePickerButton.Click();
        }
        public void AddMedicine()
        {
            AddMedicineButton.Click();
        }
        public string GetToastrMessage()
        {
            IWebElement toastrMessage = _driver.FindElement(By.ClassName("toast-message"));
            return toastrMessage.Text;
        }
    }
}
