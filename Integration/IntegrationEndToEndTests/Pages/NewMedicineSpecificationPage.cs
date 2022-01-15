using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEndToEndTests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IntegrationEndToEndTests.Pages
{
    public class NewMedicineSpecificationPage : BasePage
    {
        private IWebElement MedicineNameElement => _driver.FindElement(By.Id("medicineName"));
        //private IWebElement PharmacyElement => _driver.FindElement(By.Id("pharmacy"));
        //private IWebElement firstSelect => _driver.FindElement(By.XPath("//select[contains([@id='specificationsTable', '__TypeID')][1]"));
        private IWebElement Button => _driver.FindElement(By.Id("requestButton"));
        private SelectElement PharmacySelect => new SelectElement(_driver.FindElement(By.Id("pharmacy")));
        
        public NewMedicineSpecificationPage(IWebDriver driver) : base(driver) { }

        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition =>
            {
                try
                {
                    return Button.Displayed && MedicineNameElement.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public void InsertMedicineName(string name)
        {
            this.MedicineNameElement.SendKeys(name);
        }
        public void SelectFirstPharmacy()
        {
            this.PharmacySelect.SelectByIndex(0);
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_angularBaseUrl + "new-medicine-specification-request");

        public void Submit()
        {
            Button.Click();
        }
    }
}
