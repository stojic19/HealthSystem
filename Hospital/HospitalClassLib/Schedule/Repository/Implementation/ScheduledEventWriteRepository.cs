using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventWriteRepository : WriteBaseRepository<ScheduledEvent>, IScheduledEventWriteRepository
    {
        private readonly AppDbContext _context;
        public ScheduledEventWriteRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
