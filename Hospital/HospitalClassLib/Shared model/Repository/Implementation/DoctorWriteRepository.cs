using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class DoctorWriteRepository : WriteBaseRepository<Doctor>, IDoctorWriteRepository
    {
        public DoctorWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
