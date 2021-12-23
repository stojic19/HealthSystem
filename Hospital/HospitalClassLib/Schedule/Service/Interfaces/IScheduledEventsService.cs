using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using System;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.ServiceInterface
{
    public interface IScheduledEventsService
    {
        public List<ScheduledEvent> GetFinishedUserEvents(int userId);
        public List<ScheduledEvent> GetCanceledUserEvents(int userId);
        public List<ScheduledEvent> GetUpcomingUserEvents(int userId);
        public int GetNumberOfFinishedEvents(int userId);
        void UpdateFinishedUserEvents();
        ScheduledEvent GetScheduledEvent(int eventId);
        public List<EventForSurvey> GetEventsForSurvey(int userId);
    }
}
