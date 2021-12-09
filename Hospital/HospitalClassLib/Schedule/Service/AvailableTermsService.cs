using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class AvailableTermsService
    {
        private readonly IUnitOfWork uow;
        public AvailableTermsService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public IEnumerable<TimePeriod> GetAvailableTerms(TimePeriod timePeriod, int firstRoomId, int secondRoomId, int duration)
        {
            var availableTerms = new List<TimePeriod>();
            var possibleTerms = GetPossibleTerms(timePeriod, duration);
            foreach (TimePeriod term in possibleTerms)
            {
                if (IsAvailable(term, firstRoomId) && IsAvailable(term, secondRoomId))
                    availableTerms.Add(term);
            }
            return availableTerms;
        }

        private List<TimePeriod> GetPossibleTerms(TimePeriod timePeriod, int duration)
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

        private bool IsAvailable(TimePeriod timePeriod, int roomId)
        {
            if (IsThereAnyScheduledEvent(timePeriod, roomId))
                return false;

            if (IsThereAnyTransferEvent(timePeriod, roomId))
                return false;

            if (IsThereAnyRenovationEvent(timePeriod, roomId))
                return false;

            return true;
        }

        private bool IsThereAnyScheduledEvent(TimePeriod timePeriod, int roomId)
        {

            var eventsRepo = uow.GetRepository<IScheduledEventReadRepository>();
            var scheduledEvents = eventsRepo.GetAll().ToList();

            foreach (ScheduledEvent scheduledEvent in scheduledEvents)
            {
                if (scheduledEvent.RoomId == roomId)
                {
                    if (DoDatesOverlap(scheduledEvent.StartDate, scheduledEvent.EndDate, timePeriod))
                        return true;
                }
            }

            return false;
        }

        private bool IsThereAnyTransferEvent(TimePeriod timePeriod, int roomId)
        {

            var transfersRepo = uow.GetRepository<IEquipmentTransferEventReadRepository>();
            var scheduledTransfers = transfersRepo.GetAll().ToList();

            foreach (EquipmentTransferEvent scheduledTransfer in scheduledTransfers)
            {
                if (scheduledTransfer.InitialRoomId == roomId || scheduledTransfer.DestinationRoomId == roomId)
                {
                    if (DoDatesOverlap(scheduledTransfer.StartDate, scheduledTransfer.EndDate, timePeriod))
                        return true;
                }
            }

            return false;
        }

        private bool IsThereAnyRenovationEvent(TimePeriod timePeriod, int roomId)
        {

            var renovationsRepo = uow.GetRepository<IRoomRenovationEventReadRepository>();
            var renovationEvents = renovationsRepo.GetAll().ToList();

            foreach (RoomRenovationEvent renovationEvent in renovationEvents)
            {
                if (renovationEvent.RoomId == roomId || renovationEvent.MergeRoomId == roomId)
                {
                    if (DoDatesOverlap(renovationEvent.StartDate, renovationEvent.EndDate, timePeriod))
                        return true;
                }
            }

            return false;
        }

        private bool DoDatesOverlap(DateTime startDate, DateTime endDate, TimePeriod timePeriod)
        {
            if (DateTime.Compare(startDate, timePeriod.StartTime) == 0)
                return true;

            if (DateTime.Compare(startDate, timePeriod.StartTime) < 0)
            {
                if (DateTime.Compare(endDate, timePeriod.StartTime) > 0)
                    return true;
            }

            if (DateTime.Compare(startDate, timePeriod.StartTime) > 0
                && (DateTime.Compare(timePeriod.EndTime, endDate) > 0))
                return true;

            return false;
        }
    }
}
