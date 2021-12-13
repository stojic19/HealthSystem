using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using System;

namespace Hospital.Schedule.Model
{
    public class ScheduledEvent
    {
        public int Id { get; set; }
        public ScheduledEventType ScheduledEventType { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }    
        public DateTime CancellationDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
    }
}
