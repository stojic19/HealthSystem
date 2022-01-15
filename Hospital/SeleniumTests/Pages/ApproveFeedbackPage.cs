using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using SeleniumTests.Base;

namespace SeleniumTests.Pages
{
    public class ApproveFeedbackPage : BasePage
    {
        private readonly IWebDriver driver;
        private readonly string _uri;
        private IWebElement ApproveButton => driver.FindElement(By.Name("approve"));

        private IWebElement UnapproveButton => driver.FindElement(By.Name("unapprove"));

        public ApproveFeedbackPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this._uri = _baseUrl + "/feedbacks";
        }

        public string GetUrl()
        {
            return _baseUrl + "/feedbacks";
        }

        public void Approve()
        {
           ApproveButton.Click();
           EnsureUnapproveButtonIsDisplayed();
        }
        public void Navigate() => driver.Navigate().GoToUrl(_uri);
        
        public bool UnapproveButtonDisplayed()
        {
            return UnapproveButton.Displayed;
        }
        public bool IsSnackBarDisplayed()
        {
            try
            {
                return driver.FindElement(By.ClassName("mat-snack-bar-container")).Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return ApproveButton.Displayed;
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
        public void EnsureUnapproveButtonIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(condition =>
            {
                try
                {
                    return UnapproveButton.Displayed;
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

    }
}
