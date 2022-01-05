using System;
using Integration.Database.EfStructures;
using Integration.EventStoring.Model;
using Integration.Shared.Repository.Base;

namespace Integration.EventStoring.Repository.Implementation
{
    public class StoredEventReadRepository : ReadBaseRepository<Guid, StoredEvent>, IStoredEventReadRepository
    {
        public StoredEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
