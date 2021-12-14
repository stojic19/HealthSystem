using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;

namespace Hospital.Schedule.Repository
{
    public interface IScheduledEventReadRepository : IReadBaseRepository<int, ScheduledEvent>
    {
        List<ScheduledEvent> GetFinishedUserEvents(int userId);
        List<ScheduledEvent> GetCanceledUserEvents(int userId);
        List<ScheduledEvent> GetUpcomingUserEvents(int userId);
        int GetNumberOfFinishedEvents(int userId);
        List<ScheduledEvent> UpdateFinishedUserEvents();
        ScheduledEvent GetScheduledEvent(int eventId);
    }
}
