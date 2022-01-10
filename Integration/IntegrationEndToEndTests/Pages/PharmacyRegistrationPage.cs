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
        private IWebElement Name => _driver.FindElement(By.Id("name"));  
        private IWebElement StreetNumber => _driver.FindElement(By.Id("streetNumber"));  
        private IWebElement StreetName => _driver.FindElement(By.Id("streetName"));  
        private IWebElement CityName => _driver.FindElement(By.Id("cityName"));  
        private IWebElement PostalCode => _driver.FindElement(By.Id("postalCode"));  
        private IWebElement Country => _driver.FindElement(By.Id("country"));  
        private IWebElement BaseUrl => _driver.FindElement(By.Id("baseUrl"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submitButton"));

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

        public void InsertName(string name)
        {
            this.Name.SendKeys(name);
        }
        public void InsertStreetNumber(string streetNumber)
        {
            this.StreetNumber.SendKeys(streetNumber);
        }
        public void InsertStreetName(string streetName)
        {
            this.StreetName.SendKeys(streetName);
        }
        public void InsertCityName(string cityName)
        {
            this.CityName.SendKeys(cityName);
        }
        public void InsertPostalCode(string postalCode)
        {
            this.PostalCode.SendKeys(postalCode);
        }
        public void InsertCountry(string country)
        {
            this.Country.SendKeys(country);
        }
        public void InsertBaseUrl(string baseUrl)
        {
            this.BaseUrl.SendKeys(baseUrl);
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_angularBaseUrl + "pharmacy-register");

        public void Submit()
        {
            SubmitButton.Click();
        }
    }
}
