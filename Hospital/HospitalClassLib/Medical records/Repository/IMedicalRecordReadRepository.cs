using Hospital.Medical_records.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Medical_records.Repository
{
    public interface IMedicalRecordReadRepository : IReadBaseRepository<int, MedicalRecord>
    {
    }
}
