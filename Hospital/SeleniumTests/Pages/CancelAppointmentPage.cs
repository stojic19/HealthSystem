using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace SeleniumTests.Pages
{
    class CancelAppointmentPage
    {
        private readonly IWebDriver driver;
        public readonly string URI = "http://localhost:4200/record";
        private IWebElement CancelButton => driver.FindElement(By.Id("1b"));      
        private IWebElement MatTable => driver.FindElement(By.Id("futureAppointmentsMatTab"));
        private ReadOnlyCollection<IWebElement> Rows => driver.FindElements(By.XPath("//table[@id='futureAppointmentsMatTable']/tbody/tr"));
        private readonly int InitialRowCount;
        private TimeSpan timeOutInSeconds = new(0,0,20);

        public CancelAppointmentPage(IWebDriver driver)
        {
            this.driver = driver;
            this.InitialRowCount = RowCount();
        }
        #region Display
        public bool MatTablelementDisplayed()
        {
            return MatTable.Displayed;
        }

        public bool CancelButtonDisplayed()
        {
          //Sta ako nema nista ?
           return CancelButton.Displayed;
            
        }
        #endregion

        private int RowCount()
        {
            return Rows.Count;
        }

        internal void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return MatTablelementDisplayed();
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
        internal void EnsureMatTableIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return MatTablelementDisplayed();
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
        internal void EnsureCancelButtonIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(condition =>
            {
                try
                {
                    return CancelButtonDisplayed();
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
        internal void CancelAppointment()
        {
            EnsureCancelButtonIsDisplayed();
            WebDriverWait wait = new WebDriverWait(driver, timeOutInSeconds);
            CancelButton.SendKeys(Keys.Return);
            EnsureMatTableIsDisplayed();
        }

        public bool ElementNumberChanged()
        {
            System.Diagnostics.Debug.WriteLine(InitialRowCount);
            System.Diagnostics.Debug.WriteLine(RowCount());
           if (InitialRowCount - RowCount() == 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
 }

