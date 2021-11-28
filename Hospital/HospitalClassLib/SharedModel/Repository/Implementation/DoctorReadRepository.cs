using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class DoctorReadRepository : ReadBaseRepository<int, Doctor>, IDoctorReadRepository
    {
        public DoctorReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
