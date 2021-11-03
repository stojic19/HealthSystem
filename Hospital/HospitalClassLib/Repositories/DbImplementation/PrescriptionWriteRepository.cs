using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class PrescriptionWriteRepository : WriteBaseRepository<Prescription>, IPrescriptionWriteRepository
    {
        public PrescriptionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
