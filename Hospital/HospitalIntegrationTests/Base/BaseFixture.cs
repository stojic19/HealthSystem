﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hospital.EfStructures;
using Hospital.Infrastructure;
using Hospital.Repositories.Base;
using Hospital.Repositories.DbImplementation;
using Microsoft.EntityFrameworkCore;

namespace HospitalIntegrationTests.Base
{
    public class BaseFixture : IDisposable
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

        private void ConfigureHttpClient()
        {
            CookieContainer = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer
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
                Namespace = "Hospital.Repositories",
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
