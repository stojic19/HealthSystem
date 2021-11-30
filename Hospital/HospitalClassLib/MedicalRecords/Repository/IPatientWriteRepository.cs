using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IPatientWriteRepository : IWriteBaseRepository<Patient>
    {
    }
}
