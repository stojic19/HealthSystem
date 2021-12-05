using Hospital.Model;
using System.Collections.Generic;


namespace Hospital.Services
{
    public interface IScheduledEventsService
    {
        public List<ScheduledEvent> getFinishedUserEvents(int userId);

        public int getNumberOfFinishedEvents(int userId);
    }
}
