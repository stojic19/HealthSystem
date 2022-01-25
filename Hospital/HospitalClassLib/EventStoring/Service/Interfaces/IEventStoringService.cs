using Hospital.EventStoring.Model.Wrappers;
using System.Collections.Generic;

namespace Hospital.EventStoring.Service.Interfaces
{
    public interface IEventStoringService
    {
        public StartSchedulingPerPartOfDay GetStatisticsPerPartOfDay();
        public SuccessSchedulingPerDayOfWeek GetStatisticsPerDayOfWeek();
        public IEnumerable<int> GetStatisticsPerMonths();
        public IEnumerable<NumberOfScheduling> GetNumberOfSteps();
    }
}
