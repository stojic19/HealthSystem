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
        private const int StartHour = 7;
        private const int EndHour = 19;
        public ScheduledEventReadRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate)
        {
            var allAppointments = new List<DateTime>();
            for (var date = preferredDate; date < preferredDate.AddHours(19); date = date.AddHours(1))
            {
                if (date.Hour is > StartHour and < EndHour)
                {
                    allAppointments.Add(date);
                }
            }

            var scheduledEvents = _context.Set<ScheduledEvent>().AsEnumerable();
            var scheduledAppointments = (from appointment in allAppointments from scheduledEvent in scheduledEvents where (scheduledEvent.DoctorId == doctorId && DateTime.Compare(scheduledEvent.StartDate, appointment) == 0) select appointment);

            return allAppointments.Except(scheduledAppointments);
        }
    }
}
