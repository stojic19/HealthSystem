using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class MedicalRecordReadRepository : ReadBaseRepository<int, MedicalRecord>, IMedicalRecordReadRepository
    {
        public MedicalRecordReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
