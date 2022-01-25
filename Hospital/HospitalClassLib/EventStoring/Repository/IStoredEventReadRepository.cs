using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.EventStoring.Model;
using Hospital.EventStoring.Model.Enumerations;
using Hospital.EventStoring.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.EventStoring.Repository
{
    public interface IStoredEventReadRepository : IReadBaseRepository<Guid, StoredEvent>
    {
        public StartSchedulingPerPartOfDay GetStatisticsPerPartOfDay();
        public IEnumerable<int> GetStatisticsPerDayOfWeek(DayOfWeek day);
        public IEnumerable<int> GetStatisticsPerMonths();
        public IEnumerable<StoredEvent> GetAllByStep(Step step);

    }
}
