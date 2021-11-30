using System;

namespace HospitalApi.DTOs
{
    public class AvailableTermDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int RoomId { get; set; }
    }
}
