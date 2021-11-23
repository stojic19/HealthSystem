using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IRoomInventoryReadRepository : IReadBaseRepository<int, RoomInventory>
    {
        RoomInventory GetByRoomAndInventoryItem(int? roomId, int? inventoryItemId);
    }
}
