using System;
using System.Collections.Generic;
using Hospital.EventStoring.Model;
using Hospital.EventStoring.Model.Enumerations;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.EventStoring.Repository
{
    public interface IStoredEventReadRepository : IReadBaseRepository<Guid, StoredEvent>
    {
        public IEnumerable<int> GetStatisticsPerPartOfDay();
        public IEnumerable<int> GetStatisticsPerDayOfWeek(DayOfWeek day);
        public IEnumerable<int> GetStatisticsPerMonths();
        public IEnumerable<StoredEvent> GetAllByStep(Step step);

    }
}
