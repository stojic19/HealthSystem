using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using System;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.ServiceInterface
{
    public interface IScheduledEventsService
    {
        public List<ScheduledEvent> getFinishedUserEvents(int userId);
        public List<ScheduledEvent> getCanceledUserEvents(int userId);
        public List<ScheduledEvent> getUpcomingUserEvents(int userId);
        public int getNumberOfFinishedEvents(int userId);
        void updateFinishedUserEvents();
        ScheduledEvent GetScheduledEvent(int eventId);
        public List<EventForSurvey> getEventsForSurvey(int userId);
        String CancelScheduledEvent(int eventId);
    }
}
