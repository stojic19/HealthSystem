using Hospital.Database.EfStructures;
using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository.Implementation
{
    public class StaffReadRepository : ReadBaseRepository<int, Staff>, IStaffReadRepository
    {
        public StaffReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
