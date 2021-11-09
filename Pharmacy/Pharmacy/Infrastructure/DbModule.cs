using Autofac;
using Pharmacy.EfStructures;

namespace Pharmacy.Infrastructure
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>()
                .WithParameter("options", AppDbContextFactory.GetOptions())
                .InstancePerLifetimeScope();
        }
    }
}
