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
    public class StoredEventWriteRepository : WriteBaseRepository<StoredEvent>, IStoredEventWriteRepository
    {
        public StoredEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
