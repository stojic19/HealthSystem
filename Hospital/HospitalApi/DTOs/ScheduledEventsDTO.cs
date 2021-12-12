using System;

namespace HospitalApi.DTOs
{
    public class ScheduledEventsDTO
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}