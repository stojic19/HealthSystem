using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class MedicationWriteRepository : WriteBaseRepository<Medication>, IMedicationWriteRepository
    {
        public MedicationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
