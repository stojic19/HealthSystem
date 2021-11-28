using Autofac;
using Hospital.Database.EfStructures;

namespace Hospital.Database.Infrastructure
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
