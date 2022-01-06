using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IntegrationEndToEndTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/login";
        private IWebElement LoginUsername => _driver.FindElement(By.Id("loginUsername"));
        private IWebElement LoginPassword => _driver.FindElement(By.Id("loginPassword"));
        private IWebElement Button => _driver.FindElement(By.Id("submitButton"));

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void WaitForDisplay()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition => LoginUsername.Displayed && LoginPassword.Displayed && Button.Displayed);
        }

        public void InsertUsername(string userName)
        {
            this.LoginUsername.SendKeys(userName);
        }

        public void InsertPassword(string password)
        {
            this.LoginPassword.SendKeys(password);
        }

        public void Submit()
        {
            Button.Click();
        }
        public void Navigate() => _driver.Navigate().GoToUrl(Uri);
    }
}
