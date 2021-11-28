using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationWriteRepository : WriteBaseRepository<Medication>, IMedicationWriteRepository
    {
        public MedicationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
