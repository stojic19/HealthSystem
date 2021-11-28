namespace Hospital.Shared_model.Repository.Base
{
    public interface IUnitOfWork
    {
        T GetRepository<T>();

        int SaveChanges();
    }
}
