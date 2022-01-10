using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceTe.DynamicPDF.PageElements;
using IntegrationEndToEndTests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IntegrationEndToEndTests.Pages
{
    public class PharmacyListPage : BasePage
    {
        private ReadOnlyCollection<IWebElement> Rows =>
            _driver.FindElements(By.XPath("//table[@id='pharmaciesTable']/tbody/tr"));

        private IWebElement Table => _driver.FindElement(By.Id("pharmaciesTable"));
        public PharmacyListPage(IWebDriver driver) : base(driver)
        {
        }
        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition =>
            {
                try
                {
                    return Table.Displayed && Rows.Count > 0;
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

        public int PharmaciesCount()
        {
            return Rows.Count;
        }
        public void Navigate() => _driver.Navigate().GoToUrl(_angularBaseUrl + "pharmacy-list");
    }
}
