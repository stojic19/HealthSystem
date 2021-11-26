using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class EquipmentTransferEvent
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? InitalRoomId { get; set; }
        public Room InitalRoom { get; set; }
        public int? DestinationRoomId { get; set; }
        public Room DestinationRoom { get; set; }
        public int? InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public int Quantity { get; set; }
    }
}
