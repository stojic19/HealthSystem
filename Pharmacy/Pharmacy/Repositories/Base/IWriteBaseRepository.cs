using System.Collections.Generic;

namespace Pharmacy.Repositories.Base
{
    public interface IWriteBaseRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity, bool persist = true);
        TEntity Update(TEntity entity, bool persist = true);
        void Delete(TEntity entity, bool persist = true);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities, bool persist = true);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities, bool persist = true);
        void DeleteRange(IEnumerable<TEntity> entities, bool persist = true);
    }
}
