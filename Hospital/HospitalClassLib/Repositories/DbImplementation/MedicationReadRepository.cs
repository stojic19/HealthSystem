using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class MedicationReadRepository : ReadBaseRepository<int, Medication>, IMedicationReadRepository
    {
        public MedicationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
