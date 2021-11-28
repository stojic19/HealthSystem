using System;
using Hospital.Rooms_and_equipment.Model;

namespace Hospital.Medical_records.Model
{
    public class HospitalTreatment
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }
        public Room Room { get; set; }
        
        public MedicalRecord MedicalRecord { get; set; }
    }
}
