using System;
using Hospital.EventStoring.Model.Wrappers;
using Hospital.EventStoring.Repository;
using Hospital.EventStoring.Service.Interfaces;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Hospital.EventStoring.Model.Enumerations;

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
        private IEnumerable<SuccessScheduling> GetStartAndEndForSuccessScheduling()
        {
            var steps = new List<SuccessScheduling>();
            var startSchedulingEvents = _uow.GetRepository<IStoredEventReadRepository>().GetAllStarted().ToList();
            var scheduledEvents = _uow.GetRepository<IStoredEventReadRepository>().GetAllScheduled().ToList();
            foreach (var eventScheduled in scheduledEvents)
            {
                var schedulingEvents = startSchedulingEvents.ToList();
                foreach (var startScheduling in schedulingEvents.Where(startScheduling => eventScheduled.Username.Equals(startScheduling.Username) && (startScheduling.Time.AddMinutes(1) >= eventScheduled.Time)))
                {
                    steps.Add(new SuccessScheduling
                    {
                        EndScheduling = eventScheduled.Time,
                        StartScheduling = startScheduling.Time,
                        Username = eventScheduled.Username
                    });
                    break;
                }
            }

            return steps;
        }


        private IEnumerable<int> GetNumberStepsForSuccessScheduling()
        {
            return GetStartAndEndForSuccessScheduling().Select(eventStored => Enumerable.Count(_uow.GetRepository<IStoredEventReadRepository>().GetAll(), storedEvent => storedEvent.Username.Equals(eventStored.Username) && (eventStored.StartScheduling < storedEvent.Time) && (eventStored.EndScheduling > storedEvent.Time) && !storedEvent.Step.Equals(Step.Schedule) && !storedEvent.Step.Equals(Step.StartScheduling))).Select(counter => counter + 2).ToList();
        }

        public IEnumerable<NumberOfScheduling> GetNumberOfSteps()
        {
            var result = new List<NumberOfScheduling>();
            for (var i = 5; i <= 10; i++)
            {
                result.Add(new NumberOfScheduling
                {
                    NumberOfSteps = i,
                    NumberOfScheduled = GetNumberStepsForSuccessScheduling().Count(steps => steps == i)
                });
            }

            return result;
        }


    }
}
