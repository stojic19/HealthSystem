using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IHospitalTreatmentReadRepository : IReadBaseRepository<int, HospitalTreatment>
    {
    }
}
