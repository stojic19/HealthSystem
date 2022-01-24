using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class DeletingShiftsPage : BasePage
    {
        private readonly IWebDriver _driver;

        public readonly string URI;
        private IWebElement DeleteButton => _driver.FindElement(By.Id("deleteShift"));
        private IWebElement OkButton => _driver.FindElement(By.Id("okButton"));
        private IWebElement ConfirmModal => _driver.FindElement(By.Id("exampleModal"));
        private IWebElement Table => _driver.FindElement(By.Id("shiftTable"));
        private ReadOnlyCollection<IWebElement> Rows => _driver.FindElements(By.XPath("//table[@id='shiftTable']/tbody/tr"));
        public int InitialRowsCount;
        public DeletingShiftsPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            this.URI = base._baseUrlMan + "/hospitalShifts";
        }

        public void Navigate() => _driver.Navigate().GoToUrl(URI);

        public void DeleteShift()
        {
            EnsurePageIsDisplayed();
            EnsureDeleteButtonIsDisplayed();
            DeleteSelected();
            EnsureDialogIsDisplayed();
            Thread.Sleep(2000);
            EnsureOkButtonIsDisplayed();
            var Ok =_driver.FindElement(By.Id("okButton"));
            Ok.Click();
            Thread.Sleep(5000);
            EnsurePageIsDisplayed();
        }

        private void DeleteSelected()
        {
            InitialRowsCount = Rows.Count;
            foreach (var row in Rows)
            {
                var cols = _driver.FindElements(By.XPath("//table[@id='shiftTable']/tbody/tr/td"));
                for (int i = 0; i < cols.Count; i++)
                {
                    if (cols[i].Text.Trim().ToLower().Contains("test delete shift"))
                    {
                        cols[i + 3].FindElement(By.Id("deleteShift")).Click();
                        return;
                    }
                }
            }
        }

        public int EventsCount()
        {
            return Rows.Count;
        }

        public bool EventsNumberChanged()
        {
            if (InitialRowsCount - EventsCount() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return Table.Displayed;
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
        public void EnsureDialogIsDisplayed()
        {
            IWebElement modal = _driver.FindElement(By.Id("exampleModal"));
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return modal.Displayed;
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

        public void EnsureOkButtonIsDisplayed()
        {
            IWebElement ok = _driver.FindElement(By.Id("okButton"));
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return ok.Displayed;
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

        internal void EnsureDeleteButtonIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return DeleteButton.Displayed;
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
