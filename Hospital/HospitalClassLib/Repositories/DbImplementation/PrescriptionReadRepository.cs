using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class PrescriptionReadRepository : ReadBaseRepository<int, Prescription>, IPrescriptionReadRepository
    {
        public PrescriptionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
