using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.RoomsAndEquipment.Repository
{
    public interface IInventoryItemReadRepository : IReadBaseRepository<int, InventoryItem>
    {
    }
}
