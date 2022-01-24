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
        private int InitialRowsCount;


        public CancelRenovationPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            this.URI = base._baseUrlMan + "/schedule/";
        }

        public void Navigate(int testRoomId) => _driver.Navigate().GoToUrl(URI + testRoomId);
        public void CancelEvent()
        {
            EnsureCancelButtonIsDisplayed();
            CancelSelectedEvent();
            EnsureConfirmDialogIsOpened();
            ConfirmButton.Click();
            Thread.Sleep(10000);
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

        internal int EventsCount()
        {
            return Rows.Count;
        }

        private void CancelSelectedEvent()
        {
            InitialRowsCount = Rows.Count;
            foreach (var row in Rows)
            {
                var cols = _driver.FindElements(By.XPath("//table[@id='eventTable']/tbody/tr/td"));
                for (int i = 0; i < cols.Count; i++)
                {
                    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day);
                    if (cols[i].Text.Trim().Contains("Renovation") &&
                        cols[i + 1].Text.Trim().Contains((DateTime.Now.AddDays(3).Day + ".0" + DateTime.Now.Month +
                                                                "." + DateTime.Now.Year + " 00:00")))
                    {
                        cols[i + 3].FindElement(By.Id("cancelButton")).Click();
                        return;
                    }
                }
            }
        }
    }
}
