using Autofac;
using Integration.EfStructures;

namespace Integration.Infrastructure
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
