using Integration.Database.EfStructures;
using Microsoft.EntityFrameworkCore;

namespace Integration.Shared.Repository.Base
{
    public abstract class ReadBaseRepository<TKey, TEntity> : IReadBaseRepository<TKey, TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _context;

        protected ReadBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity GetById(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public DbSet<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }
    }
}
