using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class PatientReadRepository : ReadBaseRepository<int, Patient>, IPatientReadRepository
    {
        public PatientReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
