using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories.Base
{
    public interface IReadBaseRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);

        DbSet<TEntity> GetAll();
    }
}
