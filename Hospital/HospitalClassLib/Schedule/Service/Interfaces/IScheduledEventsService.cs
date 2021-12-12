using Hospital.Schedule.Model;
using System.Collections.Generic;

namespace Hospital.Schedule.Service.ServiceInterface
{
    public interface IScheduledEventsService
    {
        public List<ScheduledEvent> getFinishedUserEvents(int userId);

        public int getNumberOfFinishedEvents(int userId);
    }
}
