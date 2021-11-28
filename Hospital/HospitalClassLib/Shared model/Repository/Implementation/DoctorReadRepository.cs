using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class DoctorReadRepository : ReadBaseRepository<int, Doctor>, IDoctorReadRepository
    {
        public DoctorReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
