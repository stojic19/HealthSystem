﻿using System;
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
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/pharmacy-register";
        private IWebElement Name => _driver.FindElement(By.Id("name"));  
        private IWebElement StreetNumber => _driver.FindElement(By.Id("streetNumber"));  
        private IWebElement StreetName => _driver.FindElement(By.Id("streetName"));  
        private IWebElement CityName => _driver.FindElement(By.Id("cityName"));  
        private IWebElement PostalCode => _driver.FindElement(By.Id("postalCode"));  
        private IWebElement Country => _driver.FindElement(By.Id("country"));  
        private IWebElement BaseUrl => _driver.FindElement(By.Id("baseUrl"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submitButton"));

        public PharmacyRegistrationPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition => SubmitButton.Displayed && BaseUrl.Displayed);
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

        public void Navigate() => _driver.Navigate().GoToUrl(Uri);

        public void Submit()
        {
            SubmitButton.Click();
        }
    }
}
