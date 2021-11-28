using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicalRecordReadRepository : ReadBaseRepository<int, MedicalRecord>, IMedicalRecordReadRepository
    {
        public MedicalRecordReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
