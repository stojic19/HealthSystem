using System.Linq;
using Hospital.Database.EfStructures;
using Hospital.Rooms_and_equipment.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Rooms_and_equipment.Repository.Implementation
{
    public class RoomInventoryReadRepository : ReadBaseRepository<int, RoomInventory>, IRoomInventoryReadRepository
    {
        public RoomInventoryReadRepository(AppDbContext context) : base(context)
        {
        }

        public RoomInventory GetByRoomAndInventoryItem(int? roomId, int? inventoryItemId)
        {
            var roomInventories = GetAll()
                                  .Where(roomInventory => roomInventory.RoomId == roomId 
                                         && roomInventory.InventoryItemId == inventoryItemId);

            return roomInventories.FirstOrDefault();
        }
    }
}
