using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;


namespace Hospital.Schedule.Repository
{
    public interface IScheduledEventReadRepository : IReadBaseRepository<int, ScheduledEvent>
    {
        List<ScheduledEvent> GetFinishedUserEvents(string userName );
        List<ScheduledEvent> GetCanceledUserEvents(string userName);
        List<ScheduledEvent> GetUpcomingUserEvents(string userName);
        int GetNumberOfFinishedEvents(int userId);
        List<ScheduledEvent> UpdateFinishedUserEvents();
        ScheduledEvent GetScheduledEvent(int eventId);
        List<ScheduledEvent> GetNumberOfCanceledEventsForPatient(int id);
        public IEnumerable<ScheduledEvent> GetScheduledEventsForDoctor(int doctorId);
    }
}
