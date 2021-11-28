using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventReadRepository : ReadBaseRepository<int, ScheduledEvent>, IScheduledEventReadRepository
    {
        public ScheduledEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
