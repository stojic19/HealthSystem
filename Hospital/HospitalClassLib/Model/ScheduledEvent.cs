using Hospital.Model.Enumerations;
using System;

namespace Hospital.Model
{
    public class ScheduledEvent
    {
        public int Id { get; set; }

        public ScheduledEventType ScheduledEventType { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? PatientId { get; set; }
        
        public Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int? RoomId { get; set; }
        public Room Room { get; set; }
    }
}
