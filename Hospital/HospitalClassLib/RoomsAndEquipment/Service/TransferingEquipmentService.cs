using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.RoomsAndEquipment.Service
{
    public class TransferingEquipmentService
    {
        private readonly IUnitOfWork uow;
        public TransferingEquipmentService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public IEnumerable<TimePeriod> GetAvailableTerms(TimePeriod timePeriod, int initialRoomId, int destinationRoomId, int duration)
        {
            var availableTerms = new List<TimePeriod>();
            var possibleTerms = GetPossibleTerms(timePeriod, duration);
            foreach (TimePeriod term in possibleTerms)
            {
                if (IsAvailable(term, initialRoomId) && IsAvailable(term, destinationRoomId))
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
        public void StartEquipmentTransferEvent() {
            var repo = uow.GetRepository<IEquipmentTransferEventReadRepository>();
            foreach (EquipmentTransferEvent transferEvent in repo.GetAll().ToList()) {
                if (DateTime.Compare(transferEvent.EndDate, DateTime.Now) <= 0) {
                    ExecuteTransfer(transferEvent);
                }
            }
        }

        private void ExecuteTransfer(EquipmentTransferEvent transferEvent) {
            var initialRoom = uow.GetRepository<IRoomInventoryReadRepository>()
                .GetByRoomAndInventoryItem(transferEvent.InitalRoomId, transferEvent.InventoryItemId);

            var destinationRoom = uow.GetRepository<IRoomInventoryReadRepository>()
                .GetByRoomAndInventoryItem(transferEvent.DestinationRoomId, transferEvent.InventoryItemId);

            TransferFromInitialRoom(initialRoom, transferEvent);
            TransferToDestinationRoom(destinationRoom, transferEvent);

            uow.GetRepository<IEquipmentTransferEventWriteRepository>()
                .Delete(transferEvent);
        }

        private void TransferToDestinationRoom(RoomInventory destinationRoom, EquipmentTransferEvent transferEvent)
        {
            var repo = uow.GetRepository<IRoomInventoryWriteRepository>();
            if (destinationRoom == null)
            {
                destinationRoom = new RoomInventory()
                {
                    RoomId = (int)transferEvent.DestinationRoomId,
                    InventoryItemId = (int)transferEvent.InventoryItemId,
                    Amount = transferEvent.Quantity
                };
                repo.Add(destinationRoom);
            }
            else {
                destinationRoom.Amount += transferEvent.Quantity;
                repo.Update(destinationRoom);
            }

        }

        private void TransferFromInitialRoom(RoomInventory initialRoom, EquipmentTransferEvent transferEvent)
        {
            var repo = uow.GetRepository<IRoomInventoryWriteRepository>();
            if (initialRoom.Amount == transferEvent.Quantity)
            {
                repo.Delete(initialRoom);
            }
            else
            {
                initialRoom.Amount -= transferEvent.Quantity;
                repo.Update(initialRoom);
            }
        }
    }
}
