using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class RoomRenovationEventDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? RoomId { get; set; }
        public bool IsMerge { get; set; }
        public int? MergeRoomId { get; set; }
    }
}
