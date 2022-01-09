using OpenQA.Selenium;

namespace SeleniumTests.Base
{
    public class BasePage
    {
        protected readonly IWebDriver _driver;
        public readonly string _baseUrl = "http://localhost:4200";

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}