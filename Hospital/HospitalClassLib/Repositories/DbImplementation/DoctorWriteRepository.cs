using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class DoctorWriteRepository : WriteBaseRepository<Doctor>, IDoctorWriteRepository
    {
        public DoctorWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
