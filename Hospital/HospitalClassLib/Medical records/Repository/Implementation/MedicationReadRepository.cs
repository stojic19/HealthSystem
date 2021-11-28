using Hospital.Database.EfStructures;
using Hospital.Medical_records.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Medical_records.Repository.Implementation
{
    public class MedicationReadRepository : ReadBaseRepository<int, Medication>, IMedicationReadRepository
    {
        public MedicationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
