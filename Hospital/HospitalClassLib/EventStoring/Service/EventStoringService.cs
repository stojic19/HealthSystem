using System;
using Hospital.EventStoring.Model.Wrappers;
using Hospital.EventStoring.Repository;
using Hospital.EventStoring.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.EventStoring.Service
{
    public class EventStoringService : IEventStoringService
    {

        private readonly IUnitOfWork _uow;
        public EventStoringService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public SuccessSchedulingPerDayOfWeek GetStatisticsPerDayOfWeek()
        {
            var repo = _uow.GetRepository<IStoredEventReadRepository>();
            return new SuccessSchedulingPerDayOfWeek
            {
                Monday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Monday).ToList(),
                Tuesday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Tuesday).ToList(),
                Wednesday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Wednesday).ToList(),
                Thursday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Thursday).ToList(),
                Friday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Friday).ToList(),
                Saturday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Saturday).ToList(),
                Sunday = repo.GetStatisticsPerDayOfWeek(DayOfWeek.Sunday).ToList()
            };
        }

        public IEnumerable<int> GetStatisticsPerMonths()
        {
            return _uow.GetRepository<IStoredEventReadRepository>().GetStatisticsPerMonths();
        }

        public StartSchedulingPerPartOfDay GetStatisticsPerPartOfDay()
        {
            return _uow.GetRepository<IStoredEventReadRepository>().GetStatisticsPerPartOfDay();
        }
    }
}
