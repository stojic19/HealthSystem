using Hospital.RoomsAndEquipment.Model;
using System;

namespace Hospital.MedicalRecords.Model
{
    public class HospitalTreatment
    {
        public int Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }
        public int RoomId { get; private set; }
        public Room Room { get; private set; }
        public int MedicalRecordId { get; private set; }
        public MedicalRecord MedicalRecord { get; private set; }

        public HospitalTreatment(DateTime startDate, DateTime endDate, string reason, int roomId, int medicalRecordId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            RoomId = roomId;
            MedicalRecordId = medicalRecordId;
        }

        public HospitalTreatment()
        {
        }
    }
}
