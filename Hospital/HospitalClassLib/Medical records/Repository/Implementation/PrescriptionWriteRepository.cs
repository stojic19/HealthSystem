using Hospital.Database.EfStructures;
using Hospital.Medical_records.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Medical_records.Repository.Implementation
{
    public class PrescriptionWriteRepository : WriteBaseRepository<Prescription>, IPrescriptionWriteRepository
    {
        public PrescriptionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
