using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class PrescriptionWriteRepository : WriteBaseRepository<Prescription>, IPrescriptionWriteRepository
    {
        public PrescriptionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
