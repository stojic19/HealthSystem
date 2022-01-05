using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Database.EfStructures;
using Integration.EventStoring.Model;
using Integration.Shared.Repository.Base;

namespace Hospital.EventStoring.Repository.Implementation
{
    public class StoredEventReadRepository : ReadBaseRepository<Guid, StoredEvent>, IStoredEventReadRepository
    {
        public StoredEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
