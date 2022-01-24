using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
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
    public class CancelRenovationPage : BasePage
    {
        private readonly IWebDriver _driver;

        public readonly string URI;

        private int InitialEventCount;
        private IWebElement CancelButton => _driver.FindElement(By.Id("cancelButton"));
        private IWebElement ConfirmButton => _driver.FindElement(By.Id("confirm"));
        private IWebElement Table => _driver.FindElement(By.Id("eventTable"));
        private ReadOnlyCollection<IWebElement> Rows => _driver.FindElements(By.XPath("//table[@id='eventTable']/tbody/tr"));


        public CancelRenovationPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            this.URI = base._baseUrlMan + "/schedule/";
        }

        public void Navigate(int testRoomId) => _driver.Navigate().GoToUrl(URI + testRoomId);
        public void CancelEvent()
        {
            CancelButton.Click();
            EnsureConfirmDialogIsOpened();
            ConfirmButton.Click();
            Thread.Sleep(1000);
            EnsurePageIsDisplayed();
        }

        internal void EnsurePageIsDisplayed()
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

        internal bool CancelButtonDisplayed()
        {
            return CancelButton.Displayed;
        }

        internal bool ConfirmButtonDisplayed()
        {
            return ConfirmButton.Displayed;
        }

        internal bool IsDialogDisplayed()
        {
            try
            {
                return _driver.FindElement(By.TagName("app-confirm-dialog")).Displayed;
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

        public void EnsureConfirmButtonIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            wait.Until(condition =>
            {
                try
                {
                    return ConfirmButton.Displayed;
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

        public void EnsureCancelButtonIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            wait.Until(condition =>
            {
                try
                {
                    return CancelButton.Displayed;
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
        public void EnsureConfirmDialogIsOpened()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            wait.Until(condition =>
            {
                try
                {
                    return _driver.FindElement(By.TagName("app-confirm-dialog")).Displayed;
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

        public bool EventsNumberChanged(int initialRowsCount)
        {
            if (initialRowsCount - EventsCount() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal int EventsCount()
        {
            return Rows.Count;
        }
    }
}
