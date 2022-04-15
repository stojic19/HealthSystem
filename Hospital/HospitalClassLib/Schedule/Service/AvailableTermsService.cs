using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            var possibleTerms = timePeriod.Split(duration);
            foreach (TimePeriod term in possibleTerms)
            {
                if (IsAvailable(term, firstRoomId) && IsAvailable(term, secondRoomId))
                    availableTerms.Add(term);
            }
            return availableTerms;
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
                    TimePeriod period = new(scheduledEvent.StartDate, scheduledEvent.EndDate);
                    if (timePeriod.OverlapsWith(period))
                        return true;
                }
            }

            return false;
        }

        private bool IsThereAnyTransferEvent(TimePeriod timePeriod, int roomId)
        {

            var transfersRepo = uow.GetRepository<IEquipmentTransferEventReadRepository>();
            var scheduledTransfers = transfersRepo.GetAll().Include(t => t.InitialRoomInventory)
                                                            .Include(t => t.DestinationRoomInventory).ToList();

            foreach (EquipmentTransferEvent scheduledTransfer in scheduledTransfers)
            {
                if (scheduledTransfer.InitialRoomInventory.RoomId == roomId || scheduledTransfer.DestinationRoomInventory.RoomId == roomId)
                {
                    TimePeriod period = new(scheduledTransfer.TimePeriod.StartTime, scheduledTransfer.TimePeriod.EndTime);
                    if (timePeriod.OverlapsWith(period))
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
                    TimePeriod period = new(renovationEvent.StartDate, renovationEvent.EndDate);
                    if (timePeriod.OverlapsWith(period))
                        return true;
                }
            }

            return false;
        }
    }
}
