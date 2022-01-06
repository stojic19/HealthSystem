using System;

namespace Hospital.RoomsAndEquipment.Model
{
    public class RoomInventory
    {
    
        public int Id { get; set;  }
        public int RoomId { get; set; }
        public Room Room { get; private set;  }

        public int InventoryItemId { get; private set; }
        public InventoryItem InventoryItem { get; private set; }

        public int Amount { get; private set; }

        public RoomInventory() { }

        public RoomInventory(int id, int roomId, Room room, int inventoryItemId, InventoryItem inventoryItem, int amount)
        {
            Id = id;
            RoomId = roomId;
            Room = room;
            InventoryItemId = inventoryItemId;
            InventoryItem = inventoryItem;
            Amount = amount;
            Validate();
        }

        public RoomInventory(int id, Room room, InventoryItem inventoryItem, int amount) {

            Id = id;
            Room = room;
            InventoryItem = inventoryItem;
            Amount = amount;
        }

        public RoomInventory(int id, int roomId, int inventoryItemId, int amount) {
            Id = id;
            RoomId = roomId;
            InventoryItemId = inventoryItemId;
            Amount = amount;
        }

        public RoomInventory(int roomId, int inventoryItemId, int amount)
        {
            RoomId = roomId;
            InventoryItemId = inventoryItemId;
            Amount = amount;
        }

        private void Validate()
        {
            if (double.IsNegative(Amount))
                throw new ArgumentException("Quantity of an item must be larger than 0!");
        }

        public void Add(int amount)
        {
            this.Amount += amount;
        }

        public void Substract(int amount)
        {
            this.Amount -= amount;
        }
        
    }
}
