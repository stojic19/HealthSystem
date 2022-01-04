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

        public void StartEquipmentTransferEvent()
        {
            var repo = uow.GetRepository<IEquipmentTransferEventReadRepository>();
            foreach (EquipmentTransferEvent transferEvent in repo.GetAll().ToList())
            {
                if (DateTime.Compare(transferEvent.EndDate, DateTime.Now) <= 0)
                {
                    ExecuteTransfer(transferEvent);
                }
            }
        }

        private void ExecuteTransfer(EquipmentTransferEvent transferEvent)
        {
            var initialRoom = uow.GetRepository<IRoomInventoryReadRepository>()
                .GetByRoomAndInventoryItem(transferEvent.InitialRoomId, transferEvent.InventoryItemId);

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
                destinationRoom = new RoomInventory((int)transferEvent.DestinationRoomId, (int)transferEvent.InventoryItemId, transferEvent.Quantity);
                repo.Add(destinationRoom);
            }
            else
            {
                destinationRoom.Add(transferEvent.Quantity);
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
                initialRoom.Add(-transferEvent.Quantity);
                repo.Update(initialRoom);
            }
        }
    }
}
