using System;

namespace Hospital.RoomsAndEquipment.Model
{
    public class RoomInventory
    {
        private int destinationRoomId;
        private int quantity;

        public int Id { get; set;  }
        public int RoomId { get; set; }
        public Room Room { get; private set;  }

        public int InventoryItemId { get; private set; }
        public InventoryItem InventoryItem { get; private set; }

        public int Amount { get; private set; }

        //validate, add, substract

        public RoomInventory() { }

        public RoomInventory(int id, int roomId, Room room, int inventoryItemId, InventoryItem inventoryItem, int amount)
        {
            Id = id;
            RoomId = roomId;
            Room = room;
            InventoryItemId = inventoryItemId;
            InventoryItem = inventoryItem;
            Amount = amount;
        }

        public RoomInventory(int destinationRoomId, int inventoryItemId, int quantity)
        {
            this.destinationRoomId = destinationRoomId;
            InventoryItemId = inventoryItemId;
            this.quantity = quantity;
        }

        public RoomInventory(int id, int destinationRoomId, int inventoryItemId, int quantity)
        {
            this.Id = id;
            this.destinationRoomId = destinationRoomId;
            InventoryItemId = inventoryItemId;
            this.quantity = quantity;
        }

        private void Validate()
        {
            if (double.IsNegative(Amount))
                throw new Exception();
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
