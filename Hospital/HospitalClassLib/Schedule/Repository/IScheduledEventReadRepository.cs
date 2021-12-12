using System.Collections.Generic;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Schedule.Repository
{
    public interface IScheduledEventReadRepository : IReadBaseRepository<int, ScheduledEvent>
    {
        List<ScheduledEvent> GetNumberOfCanceledEventsForPatient(int id);
    }
}
