using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.RoomsAndEquipment.Repository
{
    public interface IRoomInventoryReadRepository : IReadBaseRepository<int, RoomInventory>
    {
        RoomInventory GetByRoomAndInventoryItem(int? roomId, int? inventoryItemId);
    }
}
