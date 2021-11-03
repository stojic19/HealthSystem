namespace Hospital.Model
{
    public class RoomInventory
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }

        public int Amount { get; set; }
    }
}
