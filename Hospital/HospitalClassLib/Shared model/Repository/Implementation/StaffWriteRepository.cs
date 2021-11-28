using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class StaffWriteRepository : WriteBaseRepository<Staff>, IStaffWriteRepository
    {
        public StaffWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
