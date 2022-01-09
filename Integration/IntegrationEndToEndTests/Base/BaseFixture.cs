using Autofac;
using Integration.Database.EfStructures;
using Integration.Database.Infrastructure;
using Integration.Shared.Repository.Base;
using Integration.Shared.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace IntegrationEndToEndTests.Base
{
    public class BaseFixture : IDisposable
    {
        public AppDbContext Context { get; set; }
        public IUnitOfWork UoW { get; set; }
        private IContainer container { get; set; }
        public HttpClient Client { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public BaseFixture()
        {
            SetupAutoFacDip();
            ResolveContextAndUnitOfWork();
            ConfigureHttpClient();
        }

        private void ConfigureHttpClient()
        {
            CookieContainer = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer
            };
            Client = new HttpClient(handler);
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

        private void ResolveContextAndUnitOfWork()
        {
            Context = container.Resolve<AppDbContext>();
            UoW = container.Resolve<IUnitOfWork>();
        }

        public void Dispose()
        {
            Context.Dispose();
            container.Dispose();
            Client.Dispose();
        }
    }
}
