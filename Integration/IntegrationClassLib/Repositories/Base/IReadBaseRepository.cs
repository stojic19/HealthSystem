using Microsoft.EntityFrameworkCore;

namespace Integration.Repositories.Base
{
    public interface IReadBaseRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);

        DbSet<TEntity> GetAll();
    }
}
