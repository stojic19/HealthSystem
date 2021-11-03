using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class StaffReadRepository : ReadBaseRepository<int, Staff>, IStaffReadRepository
    {
        public StaffReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
