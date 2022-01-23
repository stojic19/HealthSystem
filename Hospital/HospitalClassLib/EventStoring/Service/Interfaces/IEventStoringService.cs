using Hospital.EventStoring.Model.Wrappers;

namespace Hospital.EventStoring.Service.Interfaces
{
    public interface IEventStoringService
    {
        public StartSchedulingPerPartOfDay GetStatisticsPerPartOfDay();
    }
}
