using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IMedicalRecordReadRepository : IReadBaseRepository<int, MedicalRecord>
    {
    }
}
