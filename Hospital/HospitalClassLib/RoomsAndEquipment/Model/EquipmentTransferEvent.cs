using System;

namespace Hospital.RoomsAndEquipment.Model
{
    public class EquipmentTransferEvent
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? InitialRoomId { get; set; }
        public Room InitialRoom { get; set; }
        public int? DestinationRoomId { get; set; }
        public Room DestinationRoom { get; set; }
        public int? InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public int Quantity { get; set; }
    }
}
