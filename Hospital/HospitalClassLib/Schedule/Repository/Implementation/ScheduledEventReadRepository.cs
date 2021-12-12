using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Schedule.Repository.Implementation
{
    public class ScheduledEventReadRepository : ReadBaseRepository<int, ScheduledEvent>, IScheduledEventReadRepository
    {
        public ScheduledEventReadRepository(AppDbContext context) : base(context)
        {
        }

        public List<ScheduledEvent> GetNumberOfCanceledEventsForPatient(int id)
        {
            return GetAll().Where(x => x.PatientId == id && x.IsCanceled).ToList();
        }
    }
}
