using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Base;

namespace SeleniumTests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly IWebDriver driver;
        private IWebElement UsernameElement => driver.FindElement(By.Id("loginUsername"));
        private IWebElement PasswordElement => driver.FindElement(By.Id("loginPassword"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("login"));

        public LoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public bool UsernameElementDisplayed()
        {
            return UsernameElement.Displayed;
        }

        public bool PasswordElementDisplayed()
        {
            return PasswordElement.Displayed;
        }

        public bool SubmitButtonElementDisplayed()
        {
            return SubmitButtonElement.Displayed;
        }

        public void InsertUsername(string username)
        {
            UsernameElement.SendKeys(username);
        }

        public void InsertPassword(string password)
        {
            PasswordElement.SendKeys(password);
        }

        public void Submit()
        {
            SubmitButtonElement.Submit();
        }

        public void EnsureLoginFormForUserIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 40));
            wait.Until(condition =>
            {
                try
                {
                    return UsernameElementDisplayed() && PasswordElementDisplayed() && SubmitButtonElementDisplayed();
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

        public void EnsureUserIsLogged(){
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 40));
            wait.Until(condition =>
            {
                try
                {
                    return driver.Url.Equals(_baseUrl + "/record");
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

        public void EnsureAdminIsLoggedIn()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 40));
            wait.Until(condition =>
            {
                try
                {
                    return driver.Url.Equals(_baseUrl + "/overview");
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

        public void Navigate() => driver.Navigate().GoToUrl(_baseUrl + "/login");
    }
}
