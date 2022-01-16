using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IScheduledEventWriteRepository : IWriteBaseRepository<ScheduledEvent>
    {
        public void CancelEvent(int eventId);
    }
}
