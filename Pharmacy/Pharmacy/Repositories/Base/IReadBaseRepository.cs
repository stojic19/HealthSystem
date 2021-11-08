using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Repositories.Base
{
    public interface IReadBaseRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);

        DbSet<TEntity> GetAll();
    }
}
