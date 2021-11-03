using Hospital.EfStructures;
using System.Collections.Generic;

namespace Hospital.Repositories.Base
{
    public abstract class WriteBaseRepository<TEntity> : IWriteBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _context;

        protected WriteBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Add(entity);

            if (persist)
                _context.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Update(entity);

            if (persist)
                _context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Remove(entity);

            if (persist)
                _context.SaveChanges();
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            _context.Set<TEntity>().AddRange(entities);

            if (persist)
                _context.SaveChanges();

            return entities;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            _context.Set<TEntity>().UpdateRange(entities);

            if (persist)
                _context.SaveChanges();

            return entities;
        }

        public void DeleteRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            _context.Set<TEntity>().RemoveRange(entities);

            if (persist)
                _context.SaveChanges();
        }
    }
}
