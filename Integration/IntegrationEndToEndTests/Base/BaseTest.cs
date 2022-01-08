using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Integration.Database.EfStructures;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Cookie = System.Net.Cookie;

namespace IntegrationEndToEndTests.Base
{
    public abstract class BaseTest : IClassFixture<BaseFixture>, IDisposable
    {
        private readonly BaseFixture _fixture;
        protected readonly IWebDriver _driver;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");
            _driver = new ChromeDriver(options);
        }

        protected IUnitOfWork UoW => _fixture.UoW;
        protected AppDbContext Context => _fixture.Context;
        public CookieContainer CookieContainer => _fixture.CookieContainer;
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
            _fixture.Dispose();
        }
    }
}
