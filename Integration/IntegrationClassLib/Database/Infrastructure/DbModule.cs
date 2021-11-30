using Autofac;
using Integration.Database.EfStructures;

namespace Integration.Database.Infrastructure
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>()
                .WithParameter("options", AbbDbContextFactory.GetOptions())
                .InstancePerLifetimeScope();
        }
    }
}
