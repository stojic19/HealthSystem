using Hospital.Database.EfStructures;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository.Implementation
{
    public class StaffWriteRepository : WriteBaseRepository<Staff>, IStaffWriteRepository
    {
        public StaffWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
