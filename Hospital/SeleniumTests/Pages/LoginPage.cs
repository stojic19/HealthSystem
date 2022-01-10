﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public readonly string LoginUri = "http://localhost:4200/login";
        public readonly string FirstPage = "http://localhost:4200/overview";

        private IWebElement UsernameElement => driver.FindElement(By.Id("loginUsername"));
        private IWebElement PasswordElement => driver.FindElement(By.Id("loginPassword"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submitButton"));

        public LoginPage(IWebDriver driver,string uri)
        {
            this.driver = driver;
            LoginUri = uri;
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

        public void EnsureLoginFormForAdminIsDisplayed()
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

        public void EnsureAdminIsLoggedIn()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 40));
            wait.Until(condition =>
            {
                try
                {
                    return driver.Url.Equals(FirstPage);
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
        public void Navigate() => driver.Navigate().GoToUrl(LoginUri);
    }
}
