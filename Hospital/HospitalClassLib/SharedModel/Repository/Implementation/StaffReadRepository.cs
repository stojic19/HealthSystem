using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class StaffReadRepository : ReadBaseRepository<int, Staff>, IStaffReadRepository
    {
        public StaffReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
