using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IMedicationInventoryReadRepository : IReadBaseRepository<int, MedicationInventory>
    {
        public MedicationInventory GetMedicationByMedicationId(int id);
    }
}
