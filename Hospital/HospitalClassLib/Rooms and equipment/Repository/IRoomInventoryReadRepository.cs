using Hospital.Rooms_and_equipment.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Rooms_and_equipment.Repository
{
    public interface IRoomInventoryReadRepository : IReadBaseRepository<int, RoomInventory>
    {
        RoomInventory GetByRoomAndInventoryItem(int? roomId, int? inventoryItemId);
    }
}
