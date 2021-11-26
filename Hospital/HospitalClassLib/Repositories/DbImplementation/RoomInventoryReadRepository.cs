using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;
using System.Linq;

namespace Hospital.Repositories.DbImplementation
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
