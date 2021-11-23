using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class TransferingEquipmentService
    {
        private readonly IUnitOfWork uow;
        public TransferingEquipmentService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public IEnumerable<TimePeriod> GetAvailableTerms(TimePeriod timePeriod, int roomId, int duration)
        {
            var freeTerms = new List<TimePeriod>();
            var possibleTerms = GetPossibleTerms(timePeriod, roomId, duration);
            foreach (TimePeriod term in possibleTerms)
            {
                if (IsFree(term, roomId))
                    freeTerms.Add(term);
            }
            return freeTerms;
        }

        private List<TimePeriod> GetPossibleTerms(TimePeriod timePeriod, int roomId, int duration)
        {
            var possibleTerms = new List<TimePeriod>();
            TimeSpan wantedInterval = timePeriod.EndTime - timePeriod.StartTime;
            double intervalInHours = wantedInterval.TotalHours;

            var term = new TimePeriod();
            term.StartTime = timePeriod.StartTime;
            term.EndTime = term.StartTime.AddHours(duration);
            possibleTerms.Add(term);

            for (int i = 0; i < intervalInHours / duration - 2; i++)
            {
                term = new TimePeriod();
                term.StartTime = possibleTerms.ElementAt(i).EndTime;
                term.EndTime = term.StartTime.AddHours(duration);
                possibleTerms.Add(term);
            }

            return possibleTerms;
        }

        private bool IsFree(TimePeriod timePeriod, int roomId)
        {
            var eventsRepo = uow.GetRepository<IScheduledEventReadRepository>();
            var scheduledEvents = eventsRepo.GetAll();
           
            foreach (ScheduledEvent scheduledEvent in scheduledEvents) {
                if(scheduledEvent.RoomId == roomId)
                {
                    if (CompareDates(scheduledEvent.StartDate, scheduledEvent.EndDate, timePeriod))
                        return false;
                }
            }

            return true;
        }

        private bool CompareDates(DateTime startDate, DateTime endDate, TimePeriod timePeriod)
        {
            if (DateTime.Compare(startDate, timePeriod.StartTime) == 0)
                return true;

            if (DateTime.Compare(startDate, timePeriod.StartTime) < 0)
            {
                if (DateTime.Compare(endDate, timePeriod.StartTime) > 0)
                    return true;
            }

            if (DateTime.Compare(startDate, timePeriod.StartTime) > 0 
                && ( DateTime.Compare(timePeriod.EndTime, endDate) > 0 ))
                return true;

            return false;
        }
    }
}
