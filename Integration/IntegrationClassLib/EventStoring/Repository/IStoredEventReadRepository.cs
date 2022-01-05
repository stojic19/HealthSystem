using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EventStoring.Model;
using Integration.Shared.Repository.Base;

namespace Hospital.EventStoring.Repository
{
    public interface IStoredEventReadRepository : IReadBaseRepository<Guid, StoredEvent>
    {
    }
}
