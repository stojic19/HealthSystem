using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class ScheduledEventWriteRepository : WriteBaseRepository<ScheduledEvent>, IScheduledEventWriteRepository
    {
        public ScheduledEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
