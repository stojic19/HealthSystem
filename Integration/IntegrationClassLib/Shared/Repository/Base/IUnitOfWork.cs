namespace Integration.Shared.Repository.Base
{
    public interface IUnitOfWork
    {
        T GetRepository<T>();

        int SaveChanges();
    }
}
