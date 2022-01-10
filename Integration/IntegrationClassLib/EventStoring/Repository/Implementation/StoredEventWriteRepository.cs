using Integration.Database.EfStructures;
using Integration.EventStoring.Model;
using Integration.Shared.Repository.Base;

namespace Integration.EventStoring.Repository.Implementation
{
    public class StoredEventWriteRepository : WriteBaseRepository<StoredEvent>, IStoredEventWriteRepository
    {
        public StoredEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
