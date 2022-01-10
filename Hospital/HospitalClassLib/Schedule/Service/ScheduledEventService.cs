using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using System;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using Hospital.Schedule.Service.Interfaces;

namespace Hospital.Schedule.Service
{
    public class ScheduledEventService : IScheduledEventService
    {
        private readonly IUnitOfWork UoW;
        private const int StartHour = 7;
        private const int EndHour = 15;
        public ScheduledEventService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public List<ScheduledEvent> GetCanceledUserEvents(string userName)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(userName);
        }

        public List<ScheduledEvent> GetFinishedUserEvents(string userName)
        {

            return UoW.GetRepository<IScheduledEventReadRepository>().GetFinishedUserEvents(userName);        

        }
        public List<EventForSurvey> GetEventsForSurvey(string userName)
        {
            var finishedEvents = UoW.GetRepository<IScheduledEventReadRepository>().GetFinishedUserEvents(userName);
            var ansveredSurveyRepo = UoW.GetRepository<IAnsweredSurveyReadRepository>();
           
            List<EventForSurvey> eventsForSurveys = new();
            finishedEvents.ForEach(e =>
            {      
                eventsForSurveys.Add(new EventForSurvey()
                {
                    scheduledEvent = e,
                    answeredSurveyId = ansveredSurveyRepo.GetSurveyId(e.Id)
                 });
            });
          
            return eventsForSurveys;
        }

        public int GetNumberOfFinishedEvents(int userId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetNumberOfFinishedEvents(userId);
        }

        public ScheduledEvent GetScheduledEvent(int eventId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetScheduledEvent(eventId);
        }

        public List<ScheduledEvent> GetUpcomingUserEvents(string userName)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetUpcomingUserEvents(userName);
        }

        public void UpdateFinishedUserEvents()
        {
            var finishedUserEvents = UoW.GetRepository<IScheduledEventReadRepository>().UpdateFinishedUserEvents();
            if (finishedUserEvents.Count != 0)
            {
                finishedUserEvents.ForEach(one => one.IsDone = true);
                UoW.SaveChanges();
            }
        }

        public IEnumerable<DateTime> GetAvailableAppointments(int doctorId, DateTime preferredDate)
        {
            var availableTerms = new List<DateTime>();
            for (var date = preferredDate.AddHours(StartHour); date < preferredDate.AddHours(EndHour); date = date.AddMinutes(30))
            {
                if(UoW.GetRepository<IScheduledEventReadRepository>().IsDoctorAvailableInTerm(doctorId, date))
                        availableTerms.Add(date);
            }
            return availableTerms;
        }

        public void CancelAppointment(int eventId)
        {
            UoW.GetRepository<IScheduledEventWriteRepository>().CancelEvent(eventId);
        }
    }
}
