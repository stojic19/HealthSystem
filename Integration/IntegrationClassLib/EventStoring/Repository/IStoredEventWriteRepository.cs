using Integration.EventStoring.Model;
using Integration.Shared.Repository.Base;

namespace Integration.EventStoring.Repository
{
    public interface IStoredEventWriteRepository : IWriteBaseRepository<StoredEvent>
    {
    }
}
