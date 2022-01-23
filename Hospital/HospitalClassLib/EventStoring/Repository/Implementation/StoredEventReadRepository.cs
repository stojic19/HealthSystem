using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Database.EfStructures;
using Hospital.EventStoring.Model;
using Hospital.EventStoring.Model.Enumerations;
using Hospital.EventStoring.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.EventStoring.Repository.Implementation
{
    public class StoredEventReadRepository : ReadBaseRepository<Guid, StoredEvent>, IStoredEventReadRepository
    {
        public StoredEventReadRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<StoredEvent> GetEventsStartedScheduling()
        {
            return GetAll().Where(se => DateTime.Compare(se.Time, DateTime.Now.AddDays(-30)) >= 0 && se.Step.Equals(Step.StartScheduling));
        }

        public StartSchedulingPerPartOfDay GetStatisticsPerPartOfDay()
        {
            return new StartSchedulingPerPartOfDay
            {
                From0To8 = GetEventsStartedScheduling().Count(se => se.Time.TimeOfDay >= new TimeSpan(0, 0, 0) && se.Time.TimeOfDay < new TimeSpan(8, 0, 0)),
                From8To12 = GetEventsStartedScheduling().Count(se => se.Time.TimeOfDay >= new TimeSpan(8, 0, 0) && se.Time.TimeOfDay < new TimeSpan(12, 0, 0)),
                From12To16 = GetEventsStartedScheduling().Count(se => se.Time.TimeOfDay >= new TimeSpan(12, 0, 0) && se.Time.TimeOfDay < new TimeSpan(16, 0, 0)),
                From16To20 = GetEventsStartedScheduling().Count(se => se.Time.TimeOfDay >= new TimeSpan(16, 0, 0) && se.Time.TimeOfDay < new TimeSpan(20, 0, 0)),
                From20To00 = GetEventsStartedScheduling().Count(se => se.Time.TimeOfDay >= new TimeSpan(20, 0, 0) && se.Time.TimeOfDay < new TimeSpan(23, 59, 59))
            };
        }
    }
}
