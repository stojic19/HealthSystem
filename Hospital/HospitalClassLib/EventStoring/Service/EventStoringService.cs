using Hospital.EventStoring.Model.Wrappers;
using Hospital.EventStoring.Repository;
using Hospital.EventStoring.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.EventStoring.Service
{
    public class EventStoringService : IEventStoringService
    {

        private readonly IUnitOfWork _uow;
        public EventStoringService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public StartSchedulingPerPartOfDay GetStatisticsPerPartOfDay()
        {
            return _uow.GetRepository<IStoredEventReadRepository>().GetStatisticsPerPartOfDay();
        }
    }
}
