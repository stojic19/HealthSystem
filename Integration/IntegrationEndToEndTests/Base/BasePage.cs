using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace IntegrationEndToEndTests.Base
{
    public class BasePage
    {
        protected readonly IWebDriver _driver;
        protected readonly string _angularBaseUrl = "http://localhost:4200/";

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}
