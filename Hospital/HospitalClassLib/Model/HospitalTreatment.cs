using System;

namespace Hospital.Model
{
    public class HospitalTreatment
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }
        public Room Room { get; set; }

        public bool IsDone { get; set; }

        public int MedicalRecordId{get; set;}
        public MedicalRecord MedicalRecord { get; set; }
    }
}
