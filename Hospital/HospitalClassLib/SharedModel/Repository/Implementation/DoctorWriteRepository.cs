using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class DoctorWriteRepository : WriteBaseRepository<Doctor>, IDoctorWriteRepository
    {
        public DoctorWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
