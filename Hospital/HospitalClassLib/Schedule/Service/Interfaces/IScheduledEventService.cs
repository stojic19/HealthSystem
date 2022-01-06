using System;
using Hospital.Schedule.Model;
using System.Collections.Generic;
using Hospital.Schedule.Model.Wrappers;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IScheduledEventService
    {
        public List<ScheduledEvent> GetFinishedUserEvents(string userName);
        public List<ScheduledEvent> GetCanceledUserEvents(string userName);
        public List<ScheduledEvent> GetUpcomingUserEvents(string userName);
        public int GetNumberOfFinishedEvents(int userId);
        public void UpdateFinishedUserEvents();
        public ScheduledEvent GetScheduledEvent(int eventId);
        public List<EventForSurvey> GetEventsForSurvey(string userName);
        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate);
        public void CancelAppointment(int eventId);
    }
}
