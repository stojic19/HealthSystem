using System;
using Hospital.Schedule.Model;
using System.Collections.Generic;
using Hospital.Schedule.Model.Wrappers;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IScheduledEventService
    {
        public List<ScheduledEvent> GetFinishedUserEvents(int userId);
        public List<ScheduledEvent> GetCanceledUserEvents(int userId);
        public List<ScheduledEvent> GetUpcomingUserEvents(int userId);
        public int GetNumberOfFinishedEvents(int userId);
        void UpdateFinishedUserEvents();
        ScheduledEvent GetScheduledEvent(int eventId);
        public List<EventForSurvey> GetEventsForSurvey(int userId);
        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate);
    }
}
