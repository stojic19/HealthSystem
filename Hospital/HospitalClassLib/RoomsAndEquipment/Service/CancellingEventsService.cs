using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
using System;

namespace Hospital.RoomsAndEquipment.Service
{
    public class CancellingEventsService
    {
        private readonly IUnitOfWork uow;
        public CancellingEventsService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        private static bool EventStartsTomorrow(DateTime startDate)
        {
            if (startDate <= DateTime.Now.AddDays(1))
                return true;

            return false;
        }

        public void CancelEquipmentTransferEvent(EquipmentTransferEvent transferEvent)
        {
            var transferEventRepo = uow.GetRepository<IEquipmentTransferEventWriteRepository>();
            if (!EventStartsTomorrow(transferEvent.TimePeriod.StartTime))
            {
                transferEventRepo.Delete(transferEvent);
            }
        }

        public void CancelRoomRenovationEvent(RoomRenovationEvent renovation)
        {
            var renovationEventRepo = uow.GetRepository<IRoomRenovationEventWriteRepository>();
            if (!EventStartsTomorrow(renovation.StartDate))
            {
                renovationEventRepo.Delete(renovation);
            }
        }
    }
}
