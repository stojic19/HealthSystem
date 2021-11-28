using System.Collections.Generic;
using Hospital.Shared_model.Model.Enumerations;

namespace Hospital.Rooms_and_equipment.Model
{
    public class InventoryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public InventoryItemType InventoryItemType { get; set; }

        public IEnumerable<RoomInventory> RoomInventories { get; set; }
    }
}
