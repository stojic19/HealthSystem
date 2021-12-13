using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;

namespace Hospital.RoomsAndEquipment.Repository
{
    public interface IRoomInventoryReadRepository : IReadBaseRepository<int, RoomInventory>
    {
        RoomInventory GetByRoomAndInventoryItem(int? roomId, int? inventoryItemId);
        IEnumerable<RoomInventory> GetByRoom(int? roomId);
    }
}
