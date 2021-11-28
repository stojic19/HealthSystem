using Microsoft.EntityFrameworkCore;

namespace Hospital.Shared_model.Repository.Base
{
    public interface IReadBaseRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);

        DbSet<TEntity> GetAll();
    }
}
