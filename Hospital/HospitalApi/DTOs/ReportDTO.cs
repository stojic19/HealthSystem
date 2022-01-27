using System;

namespace HospitalApi.DTOs
{
    public class ReportDTO
    {
        public DoctorDTO Doctor{ get; set; }
        public DateTime CreatedDate { get; set; }
        public string WrittenReport { get; set; }
        public int DoctorsId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
