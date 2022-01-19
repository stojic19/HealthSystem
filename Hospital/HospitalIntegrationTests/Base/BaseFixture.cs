﻿using Autofac;
using Hospital.Database.Infrastructure;
using Hospital.SharedModel.Repository.Base;
using Hospital.SharedModel.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace HospitalIntegrationTests.Base
{
    public class BaseFixture : IDisposable
    {
        private IContainer container { get; set; }
        public IUnitOfWork UoW { get; set; }
        public HttpClient ManagerClient { get; set; }
        public HttpClient PatientClient { get; set; }
        public CookieContainer CookieContainer { get; set; }

        public BaseFixture()
        {
            SetupAutoFacDip();
            ResolveUnitOfWork();
            ConfigureManagerHttpClient();
            ConfigurePatientHttpClient();
        }

        private void ConfigureManagerHttpClient()
        {
            CookieContainer = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer
            };
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            ManagerClient = new HttpClient(handler);
        }
        private void ConfigurePatientHttpClient()
        {
            CookieContainer = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer
            };
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            PatientClient = new HttpClient(handler);
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
            ManagerClient.Dispose();
            PatientClient.Dispose();
        }
    }
}
