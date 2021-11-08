using Autofac;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace Pharmacy.Infrastructure
{
    public class RepositoryModule : Module
    {
        public List<Assembly> RepositoryAssemblies { get; set; }
        public string Namespace { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(RepositoryAssemblies.ToArray())
                .Where(x => x.Namespace.Contains(Namespace))
                .AsImplementedInterfaces();
        }
    }
}
