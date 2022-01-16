using System;
using Integration.EventStoring.Model;
using Integration.Shared.Repository.Base;

namespace Integration.EventStoring.Repository
{
    public interface IStoredEventReadRepository : IReadBaseRepository<Guid, StoredEvent>
    {
    }
}
