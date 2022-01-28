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
                if (DateTime.Compare(transferEvent.TimePeriod.EndTime, DateTime.Now) <= 0)
                {
                    ExecuteTransfer(transferEvent);
                }
            }
        }

        private void ExecuteTransfer(EquipmentTransferEvent transferEvent)
        {
            var repo = uow.GetRepository<IRoomInventoryWriteRepository>();
            RoomInventory initialRoom = transferEvent.TransferFromInitialRoom();
            RoomInventory destinationRoom = transferEvent.TransferToDestinationRoom();
            if (initialRoom.Amount == 0)
            {
                repo.Delete(initialRoom);
            }
            else
            {
                repo.Update(initialRoom);
            }
             repo.Update(destinationRoom);
            uow.GetRepository<IEquipmentTransferEventWriteRepository>()
                .Delete(transferEvent);
        }
    }
}
