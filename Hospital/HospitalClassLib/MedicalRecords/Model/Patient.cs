using System.Buffers.Text;
using Hospital.SharedModel.Model;

namespace Hospital.MedicalRecords.Model
{
    public class Patient : User
    {
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public bool IsBlocked { get; set; }
        public string PhotoEncoded { get; set; }
    }
}
