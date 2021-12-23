using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class CreateFeedbackPage
    {
        private readonly IWebDriver _driver;
        public readonly string Uri = "http://localhost:4200/feedbacks";
        private IWebElement OpenModalElement => _driver.FindElement(By.Id("openModal"));
        private IWebElement FeedbackTextElement => _driver.FindElement(By.Id("text"));
        private IWebElement StayAnonymousElement => _driver.FindElement(By.Id("stayAnonymous"));
        private IWebElement IsPublishableElement => _driver.FindElement(By.Id("isPublishable"));
        private IWebElement SubmitElement => _driver.FindElement(By.Id("submitFeedback"));

        public CreateFeedbackPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(Uri);

        public void OpenModal() => OpenModalElement.Click();

        public void InsertFeedback(string text) => FeedbackTextElement.SendKeys(text);


        public void InsertIsAnonymous(bool isAnonymous)
        {
            if (isAnonymous)
            {
                StayAnonymousElement.Click();
            }
        }

        public void InsertIsPublishable(bool isPublishable)
        {
            if (isPublishable)
                IsPublishableElement.Click();
        }

        public void SubmitForm() => SubmitElement.Click();

        public bool IsSnackbarDisplayed()
        {
            try
            {
                return _driver.FindElement(By.ClassName("mat-snack-bar-container")).Displayed;
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

        public void WaitForDialogOpened()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("fbForm")));
        }

        public void WaitForFormSubmitted()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(condition => IsSnackbarDisplayed());
        }
    }
}