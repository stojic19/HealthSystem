//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SeleniumTests.Base;

//namespace SeleniumTests.Pages
//{
//    public class BlockMaliciousPatientsPage : BasePage
//    {
//        private readonly IWebDriver _driver;
//        public readonly string URI;
//        private IWebElement BlockButton => _driver.FindElement(By.Id("blockPatientBtn"));
//        private IWebElement Table => _driver.FindElement(By.Id("pat-tab"));
//        private ReadOnlyCollection<IWebElement> Rows => _driver.FindElements(By.XPath("//table[@id='pat-tab']/tbody/tr"));

//        public BlockMaliciousPatientsPage(IWebDriver driver) : base(driver)
//        {
//            _driver = driver;
//            URI = _baseUrlMan + "/blocking";
//        }

//        public void Navigate() => _driver.Navigate().GoToUrl(URI);
//        public void BlockPatient()
//        {
//            BlockButton.Click();
//            EnsureSnackBarIsDisplayed();
//        }

//        internal void EnsurePageIsDisplayed()
//        {
//            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
//            wait.Until(condition =>
//            {
//                try
//                {
//                    return BlockButton.Displayed;
//                }
//                catch (StaleElementReferenceException)
//                {
//                    return false;
//                }
//                catch (NoSuchElementException)
//                {
//                    return false;
//                }
//            });
//        }

//        internal bool BlockButtonDisplayed()
//        {
//            return BlockButton.Displayed;
//        }

//        internal bool IsSnackBarDisplayed()
//        {
//            try
//            {
//                return _driver.FindElement(By.ClassName("mat-snack-bar-container")).Displayed;
//            }
//            catch (StaleElementReferenceException)
//            {
//                return false;
//            }
//            catch (NoSuchElementException)
//            {
//                return false;
//            }
//        }

//        internal int PatientCount()
//        {
//            return Rows.Count;
//        }

//        public void EnsureBlockButtonIsDisplayed()
//        {
//            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
//            wait.Until(condition =>
//            {
//                try
//                {
//                    return BlockButton.Displayed;
//                }
//                catch (StaleElementReferenceException)
//                {
//                    return false;
//                }
//                catch (NoSuchElementException)
//                {
//                    return false;
//                }
//            });
//        }
//        public void EnsureSnackBarIsDisplayed()
//        {
//            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
//            wait.Until(condition =>
//            {
//                try
//                {
//                    return _driver.FindElement(By.ClassName("mat-snack-bar-container")).Displayed;
//                }
//                catch (StaleElementReferenceException)
//                {
//                    return false;
//                }
//                catch (NoSuchElementException)
//                {
//                    return false;
//                }
//            });
//        }

//    }
//}
