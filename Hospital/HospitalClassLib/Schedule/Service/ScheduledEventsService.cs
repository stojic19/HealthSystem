using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.ServiceInterface;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Service
{
    public class ScheduledEventsService : IScheduledEventsService
    {
        private readonly IUnitOfWork UoW;
        public ScheduledEventsService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public List<ScheduledEvent> getCanceledUserEvents(int userId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(userId);
        }

        public List<ScheduledEvent> getFinishedUserEvents(int userId)
        {

            return UoW.GetRepository<IScheduledEventReadRepository>().GetFinishedUserEvents(userId);
           

        }
        public List<EventForSurvey> getEventsForSurvey(int userId)
        {

            var events = UoW.GetRepository<IScheduledEventReadRepository>().GetFinishedUserEvents(userId);
            var ansveredSurveyRepo = UoW.GetRepository<IAnsweredSurveyReadRepository>();
            List<EventForSurvey> eventsForSurveys = new List<EventForSurvey>();
            foreach (ScheduledEvent scheduledEvent in events)
            {
                var survey = ansveredSurveyRepo.GetAll().Where(x => x.ScheduledEventId == scheduledEvent.Id).FirstOrDefault();

                EventForSurvey eventForSurvey = new EventForSurvey();
                eventForSurvey.scheduledEvent = scheduledEvent;
                if (survey == null) { 

                eventForSurvey.answeredSurveyId = -1;
                }
                else
                {
                    eventForSurvey.answeredSurveyId = survey.Id;
                }
                eventsForSurveys.Add(eventForSurvey);
            }
            return eventsForSurveys;

        }

        public int getNumberOfFinishedEvents(int userId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetNumberOfFinishedEvents(userId);
        }

        public ScheduledEvent GetScheduledEvent(int eventId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetScheduledEvent(eventId);
        }

        public List<ScheduledEvent> getUpcomingUserEvents(int userId)
        {
            return UoW.GetRepository<IScheduledEventReadRepository>().GetUpcomingUserEvents(userId);
        }

        public void updateFinishedUserEvents()
        {
            var finishedUserEvents = UoW.GetRepository<IScheduledEventReadRepository>().UpdateFinishedUserEvents();

            if (finishedUserEvents.Count != 0)
            {
                finishedUserEvents.ForEach(one => one.IsDone = true);
                UoW.SaveChanges();
            }
        }

        public String CancelScheduledEvent(int eventId)
        {
            var scheduled =  UoW.GetRepository<IScheduledEventReadRepository>().GetById(eventId);
            String message = "";
            if (scheduled.IsCanceled == false && scheduled.IsDone == false)
            {
                DateTime calculated = scheduled.StartDate.AddDays(-2);

                if (DateTime.Compare(DateTime.Now, calculated) <= 0)
                {
                    scheduled.IsCanceled = true;
                    UoW.SaveChanges();
                    message = "This Appointment has been canceled.";
                }

            }
            else
            {

                message = "This Appointment cannot be canceled";
            }
            return message;
        }
    }
}
