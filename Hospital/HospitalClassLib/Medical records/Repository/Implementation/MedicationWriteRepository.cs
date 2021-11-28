using Hospital.Database.EfStructures;
using Hospital.Medical_records.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Medical_records.Repository.Implementation
{
    public class MedicationWriteRepository : WriteBaseRepository<Medication>, IMedicationWriteRepository
    {
        public MedicationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
