using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class EquipmentTransferEventDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? InitialRoomId { get; set; }
        public int? DestinationRoomId { get; set; }
        public int? InventoryItemId { get; set; }
        public int Quantity { get; set; }
    }
}
