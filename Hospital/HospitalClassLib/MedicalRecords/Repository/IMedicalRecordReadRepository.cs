using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IMedicalRecordReadRepository : IReadBaseRepository<int, MedicalRecord>
    {
        public MedicalRecord GetMedicalRecord(int id);
    }
}
