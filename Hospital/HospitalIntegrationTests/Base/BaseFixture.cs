using Autofac;
using Hospital.Database.Infrastructure;
using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Repository.Implementation;
using HospitalApi;
using HospitalIntegrationTests.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace HospitalIntegrationTests.Base
{
    public class BaseFixture : WebApplicationFactory<Startup>, IDisposable 
    {
        private IContainer container { get; set; }
        public IUnitOfWork UoW { get; set; }
        public HttpClient Client { get; set; }
        public CookieContainer CookieContainer { get; set; }

        public BaseFixture()
        {
            SetupAutoFacDip();
            ResolveUnitOfWork();
            ConfigureHttpClient();
        }

        public WebApplicationFactory<Startup> AuthenticatedInstance(params Claim[] claimSeed)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IAuthenticationSchemeProvider, MockSchemeProvider>();
                    services.AddSingleton<MockClaimSeed>(_ => new(claimSeed));
                });
            });
        }

        private void ConfigureHttpClient()
        {
            CookieContainer = new CookieContainer();
            var credentials = new NetworkCredential("andji", "Andji1234");

            var handler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer,
                Credentials = credentials

            };
           
            Client = new HttpClient(handler);
        }

        private void ResolveUnitOfWork()
        {
            UoW = container.Resolve<IUnitOfWork>();
        }

        private void SetupAutoFacDip()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DbModule());
            builder.RegisterModule(new RepositoryModule()
            {
                Namespace = "Repository",
                RepositoryAssemblies = new List<Assembly>()
                {
                    typeof(CityReadRepository).Assembly
                }
            });
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            container = builder.Build();
        }

        public void Dispose()
        {
            container.Dispose();
            Client.Dispose();
        }
    }
}
