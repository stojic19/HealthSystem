namespace Hospital.SharedModel.Repository.Base
{
    public interface IUnitOfWork
    {
        T GetRepository<T>();

        int SaveChanges();
    }
}
