using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class PrescriptionReadRepository : ReadBaseRepository<int, Prescription>, IPrescriptionReadRepository
    {
        public PrescriptionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
