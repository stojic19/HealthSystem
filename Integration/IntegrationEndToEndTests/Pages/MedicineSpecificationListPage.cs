using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEndToEndTests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IntegrationEndToEndTests.Pages
{
    public class MedicineSpecificationListPage : BasePage
    {
        private IWebElement Button => _driver.FindElement(By.Id("getNewSpecification"));

        private ReadOnlyCollection<IWebElement> Rows =>
            _driver.FindElements(By.XPath("//table[@id='specificationsTable']/tbody/tr"));

        private IWebElement Table => _driver.FindElement(By.Id("specificationsTable"));

        public MedicineSpecificationListPage(IWebDriver driver) : base(driver) { }

        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition =>
            {
                try
                {
                    return Button.Displayed && Table.Displayed && Rows.Count > 0;
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

        public int MedicineSpecificationCount()
        {
            return Rows.Count;
        }
        public void Navigate() => _driver.Navigate().GoToUrl(_angularBaseUrl + "medicine-specification-requests");


    }
}
