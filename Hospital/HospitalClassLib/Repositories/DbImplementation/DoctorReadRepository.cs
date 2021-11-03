using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class DoctorReadRepository : ReadBaseRepository<int, Doctor>, IDoctorReadRepository
    {
        public DoctorReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
