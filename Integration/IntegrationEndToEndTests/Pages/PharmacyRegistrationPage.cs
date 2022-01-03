using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IntegrationEndToEndTests.Pages
{
    public class PharmacyRegistrationPage
    {
        private readonly IWebDriver driver;
        public const string uri = "http://localhost:4200/pharmacy-register";
        private IWebElement name => driver.FindElement(By.Id("name"));  
        private IWebElement streetNumber => driver.FindElement(By.Id("streetNumber"));  
        private IWebElement streetName => driver.FindElement(By.Id("streetName"));  
        private IWebElement cityName => driver.FindElement(By.Id("cityName"));  
        private IWebElement postalCode => driver.FindElement(By.Id("postalCode"));  
        private IWebElement country => driver.FindElement(By.Id("country"));  
        private IWebElement baseUrl => driver.FindElement(By.Id("baseUrl"));
        private IWebElement submitButton => driver.FindElement(By.Id("submitButton"));

        public PharmacyRegistrationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition => submitButton.Displayed && baseUrl.Displayed);
        }

        public void InsertName(string name)
        {
            this.name.SendKeys(name);
        }
        public void InsertStreetNumber(string streetNumber)
        {
            this.streetNumber.SendKeys(streetNumber);
        }
        public void InsertStreetName(string streetName)
        {
            this.streetName.SendKeys(streetName);
        }
        public void InsertCityName(string cityName)
        {
            this.cityName.SendKeys(cityName);
        }
        public void InsertPostalCode(string postalCode)
        {
            this.postalCode.SendKeys(postalCode);
        }
        public void InsertCountry(string country)
        {
            this.country.SendKeys(country);
        }
        public void InsertBaseUrl(string baseUrl)
        {
            this.baseUrl.SendKeys(baseUrl);
        }

        public void Navigate() => driver.Navigate().GoToUrl(uri);

        public void Submit()
        {
            submitButton.Click();
        }
    }
}
