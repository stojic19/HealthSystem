using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.RoomsAndEquipment.Repository
{
    public interface IEquipmentTransferEventReadRepository : IReadBaseRepository<int, EquipmentTransferEvent>
    {
    }
}
