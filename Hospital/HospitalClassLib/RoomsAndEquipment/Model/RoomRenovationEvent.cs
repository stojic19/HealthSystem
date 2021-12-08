using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.RoomsAndEquipment.Model
{
    public class RoomRenovationEvent
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public bool IsMerge { get; set; }
        public int? MergeRoomId { get; set; }
        public Room MergeRoom { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDone { get; set; }
    }
}
