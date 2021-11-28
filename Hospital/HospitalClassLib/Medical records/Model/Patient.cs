using Hospital.Shared_model.Model;

namespace Hospital.Medical_records.Model
{
    public class Patient : User
    {
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public bool IsBlocked { get; set; }
    }
}
