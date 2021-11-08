using Autofac;
using Pharmacy.EfStructures;

namespace Pharmacy.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IComponentContext _componentContext;

        public UnitOfWork(AppDbContext context, IComponentContext componentContext)
        {
            _context = context;
            _componentContext = componentContext;
        }

        public T GetRepository<T>()
        {
            return _componentContext.Resolve<T>(new TypedParameter(typeof(T), _context));
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
