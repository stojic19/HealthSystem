using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.RoomsAndEquipment.Service
{
    public class CancellingEventsService
    {
        private readonly IUnitOfWork uow;
        public CancellingEventsService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        private bool EventStartsTomorrow(DateTime startDate)
        {
            if (startDate <= DateTime.Now.AddDays(1))
                return true;

            return false;
        }

        public void CancelEquipmentTransferEvent(EquipmentTransferEvent transferEvent)
        {
            var transferEventRepo = uow.GetRepository<IEquipmentTransferEventWriteRepository>();
            if (!EventStartsTomorrow(transferEvent.StartDate))
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
