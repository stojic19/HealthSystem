using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class StaffWriteRepository : WriteBaseRepository<Staff>, IStaffWriteRepository
    {
        public StaffWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
