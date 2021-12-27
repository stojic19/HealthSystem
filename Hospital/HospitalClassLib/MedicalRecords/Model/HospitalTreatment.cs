using Hospital.RoomsAndEquipment.Model;
using System;

namespace Hospital.MedicalRecords.Model
{
    public class HospitalTreatment
    {
        public int Id { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get;}
        public string Reason { get; }
        public Room Room { get; }
        public MedicalRecord MedicalRecord { get; }

        public HospitalTreatment(DateTime startDate, DateTime endDate, string reason, Room room, MedicalRecord medicalRecord)
        {
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            Room = room;
            MedicalRecord = medicalRecord;
        }

        public HospitalTreatment()
        {
        }
    }
}
