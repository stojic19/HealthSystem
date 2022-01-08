using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.EventStoring.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.EventStoring.Repository
{
    public interface IStoredEventReadRepository : IReadBaseRepository<Guid, StoredEvent>
    {
    }
}
