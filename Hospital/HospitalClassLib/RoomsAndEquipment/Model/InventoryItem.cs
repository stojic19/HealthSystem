using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;

namespace Hospital.RoomsAndEquipment.Model
{
    public class InventoryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public InventoryItemType InventoryItemType { get; set; }

        public IEnumerable<RoomInventory> RoomInventories { get; set; }
    }
}
