using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicalRecordWriteRepository : WriteBaseRepository<MedicalRecord>, IMedicalRecordWriteRepository
    {
        public MedicalRecordWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
