using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationReadRepository : ReadBaseRepository<int, Medication>, IMedicationReadRepository
    {
        public MedicationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
