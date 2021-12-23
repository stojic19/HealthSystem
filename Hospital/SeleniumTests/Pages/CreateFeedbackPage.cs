using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class CreateFeedbackPage
    {
        private readonly IWebDriver driver;
        private readonly string URI = "http://localhost:4200/feedbacks";
        private IWebElement OpenModalElement => driver.FindElement(By.Id("openModal"));
        private IWebElement FeedbackTextElement => driver.FindElement(By.Id("text"));
        private IWebElement StayAnonymousElement => driver.FindElement(By.Id("stayAnonymous"));
        private IWebElement IsPublishableElement => driver.FindElement(By.Id("isPublishable"));
        private IWebElement SubmitElement => driver.FindElement(By.Id("submitFeedback"));
        private IWebElement FormElement => driver.FindElement(By.Id("fbForm"));

        public CreateFeedbackPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public void OpenModal() => OpenModalElement.Click();

        public void InsertFeedback(string text) => FeedbackTextElement.SendKeys(text);


        public void InsertIsAnonymous(Boolean isAnonymous)
        {
            if (isAnonymous)
            {
                StayAnonymousElement.Click();
            }
        }

        public void InsertIsPublishable(Boolean isPublishable)
        {
            if (isPublishable)
                IsPublishableElement.Click();
        }

        public void SubmitForm() => SubmitElement.Click();

        public Boolean IsSubmitEnabled() { return SubmitElement.Enabled; }

        public Boolean IsFormElementDisplayed()
        {
            try
            {
                return FormElement.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        public void WaitForDialogOpened()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return FormElement.Displayed;
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