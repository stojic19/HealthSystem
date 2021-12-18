using System;
using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;


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
        public IEnumerable<ScheduledEvent> GetDoctorsScheduledEvents(int doctorId);
        bool IsDoctorAvailableInTerm(int doctorId, DateTime date);
        List<ScheduledEvent> GetNumberOfCanceledEventsForPatient(int id);
    }
}
