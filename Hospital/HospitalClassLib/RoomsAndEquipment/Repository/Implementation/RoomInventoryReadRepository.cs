using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class RoomInventoryReadRepository : ReadBaseRepository<int, RoomInventory>, IRoomInventoryReadRepository
    {
        public RoomInventoryReadRepository(AppDbContext context) : base(context)
        {

        }

        public IEnumerable<RoomInventory> GetByRoom(int? roomId)
        {
            var roomInventories = GetAll()
                                  .Where(roomInventory => roomInventory.RoomId == roomId);

            return roomInventories;
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
