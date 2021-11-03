using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class PatientWriteRepository : WriteBaseRepository<Patient>, IPatientWriteRepository
    {
        public PatientWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
