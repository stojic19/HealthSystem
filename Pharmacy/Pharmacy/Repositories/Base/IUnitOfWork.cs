namespace Pharmacy.Repositories.Base
{
    public interface IUnitOfWork
    {
        T GetRepository<T>();

        int SaveChanges();
    }
}
