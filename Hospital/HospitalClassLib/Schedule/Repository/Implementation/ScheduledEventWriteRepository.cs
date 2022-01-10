using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Linq;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventWriteRepository : WriteBaseRepository<ScheduledEvent>, IScheduledEventWriteRepository
    {
        private readonly AppDbContext _context;
        public ScheduledEventWriteRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public void CancelEvent(int eventId)
        {
            var scheduledEvent = _context.Set<ScheduledEvent>()
                 .Where(x => x.Id == eventId && x.IsDone == false)
                 .ToList()
                 .FirstOrDefault();
            if (scheduledEvent != null && DateTime.Compare(DateTime.Now, scheduledEvent.StartDate.AddDays(-2)) < 0)
            {
                scheduledEvent.IsCanceled = true;
                Update(scheduledEvent, true);
            }
        }
    }
}
