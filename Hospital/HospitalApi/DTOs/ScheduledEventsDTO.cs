using Hospital.SharedModel.Model;
using System;

namespace HospitalApi.DTOs
{
    public class ScheduledEventsDTO
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DoctorName { get; set; }
        public string DoctorLastName { get; set; }
        public string SpecializationName { get; set; }
        public string RoomName { get; set; }

    }
}