using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IMedicationReadRepository : IReadBaseRepository<int, Medication>
    {
        public Medication GetMedicationByName(string name);
    }
}
