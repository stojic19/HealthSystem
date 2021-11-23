using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Integration.EfStructures;
using Integration.Infrastructure;
using Integration.Model;
using Integration.Repositories.Base;
using Integration.Repositories.DbImplementation;

namespace IntegrationUnitTests.Base
{
    public class BaseFixture : IDisposable
    {
        public AppDbContext Context { get; set; }
        public IUnitOfWork UoW { get; set; }
        private IContainer container { get; set; }

        public BaseFixture()
        {
            SetupAutoFacDip();
            ResolveContextAndUnitOfWork();
        }

        private void SetupAutoFacDip()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppDbContext>()
                .WithParameter("options", GetAppDbContextOptions())
                .InstancePerLifetimeScope();

            builder.RegisterModule(new RepositoryModule()
            {
                Namespace = "Integration.Repositories",
                RepositoryAssemblies = new List<Assembly>()
                {
                    (typeof(CityReadRepository)).Assembly
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

        private DbContextOptions<AppDbContext> GetAppDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("IntegrationInMemoryDB");

            return optionsBuilder.Options;
        }

        public void Dispose()
        {
            Context.Dispose();
            container.Dispose();

        }
    }
}
