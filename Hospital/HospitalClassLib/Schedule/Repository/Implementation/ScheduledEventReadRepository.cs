using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventReadRepository : ReadBaseRepository<int, ScheduledEvent>, IScheduledEventReadRepository
    {
        private readonly AppDbContext _context;

        public ScheduledEventReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ScheduledEvent> GetDoctorsScheduledEvents(int doctorId)
        {
            return _context.Set<ScheduledEvent>().Where(x => x.DoctorId == doctorId).AsEnumerable();
        }

        public bool IsDoctorAvailableInTerm(int doctorId, DateTime date)
        {
            var scheduledEvents = GetDoctorsScheduledEvents(doctorId);
            return scheduledEvents.Where(s => DateTime.Compare(s.StartDate, date) == 0).All(s => s.IsCanceled);
        }
    }
}
