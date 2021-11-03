using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class MedicalRecordWriteRepository : WriteBaseRepository<MedicalRecord>, IMedicalRecordWriteRepository
    {
        public MedicalRecordWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
