using System;
using Hospital.Schedule.Model;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IScheduledEventService
    {
        public List<ScheduledEvent> getFinishedUserEvents(int userId);

        public int getNumberOfFinishedEvents(int userId);
        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate);
    }
}
