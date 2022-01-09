using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SeleniumTests.Pages
{
    class CancelAppointmentPage : BasePage
    {
        private readonly IWebDriver driver;

        public readonly string URI;

        private int InitialRowCount;

        private TimeSpan timeOutInSeconds = new(0, 0, 20);
        private readonly string _rowsPath = "/html/body/app-root/app-main/app-patient-medical-record/div/mat-card[3]/mat-tab-group/div/mat-tab-body[1]/div/mat-card/div/div[2]/table/tbody/tr";
        
        private IWebElement CancelButton => driver.FindElement(By.Id("1b"));
     
        private IWebElement MatTable => driver.FindElement(By.Id("futureAppointmentsMatTab"));
        private ReadOnlyCollection<IWebElement> Rows => driver.FindElements(By.XPath(_rowsPath));
        private IWebElement FirstRow => driver.FindElement(By.XPath(_rowsPath + "[1]"));
        private IWebElement LastRow => driver.FindElement(By.XPath(_rowsPath + "[last()]"));
        private IWebElement TestCancelButton => driver.FindElement(By.XPath(_rowsPath + "[last()]" + "/td[7]/button"));

        public CancelAppointmentPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.URI = base._baseUrl + "/record";

        }
        #region Display
        public bool MatTablelementDisplayed()
        {
            return MatTable.Displayed;
        }
        public bool LastRowDisplayed()
        {
            return LastRow.Displayed;
        }
        public bool FirstRowDisplayed()
        {
            return FirstRow.Displayed;
        }
        public bool TableRowsDisplayed()
        {
            return Rows.Count > 0;
        }
        public bool CancelButtonDisplayed()
        {
          //Sta ako nema nista ?
           return CancelButton.Displayed;           
        }
        public bool TestCancelButtonDisplayed()
        {
            //Sta ako nema nista ?
            return TestCancelButton.Displayed;

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
                    return MatTablelementDisplayed() && FirstRowDisplayed() && TableRowsDisplayed()
                             && LastRowDisplayed() && TestCancelButtonDisplayed();
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
            InitialRowCount = Rows.Count;
            EnsureCancelButtonIsDisplayed();
            CancelButton.SendKeys(Keys.Return);
            Thread.Sleep(5000);
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

