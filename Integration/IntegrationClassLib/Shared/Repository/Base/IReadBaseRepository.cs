using Microsoft.EntityFrameworkCore;

namespace Integration.Shared.Repository.Base
{
    public interface IReadBaseRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);

        DbSet<TEntity> GetAll();
    }
}
