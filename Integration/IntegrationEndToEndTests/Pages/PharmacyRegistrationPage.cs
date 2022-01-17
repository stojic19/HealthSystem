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
    public class PharmacyRegistrationPage : BasePage
    {
        private IWebElement BaseUrl => _driver.FindElement(By.Id("baseUrl"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submitButton"));

        public readonly string EmptyUrl = "Please enter url!";
        public readonly string InvalidUrl = "Failed to reach pharmacy, please try again!";

        public PharmacyRegistrationPage(IWebDriver driver) : base(driver){}

        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition =>
            {
                try
                {
                    return SubmitButton.Displayed && BaseUrl.Displayed;
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

        public void InsertBaseUrl(string baseUrl)
        {
            BaseUrl.SendKeys(baseUrl);
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_angularBaseUrl + "pharmacy-register");

        public void Submit()
        {
            SubmitButton.Click();
        }

        public string GetToastrMessage()
        {
            IWebElement toastrMessage = _driver.FindElement(By.ClassName("toast-message"));
            return toastrMessage.Text;
        }
    }
}
