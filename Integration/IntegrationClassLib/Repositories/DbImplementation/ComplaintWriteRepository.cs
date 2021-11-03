using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ComplaintWriteRepository : WriteBaseRepository<Complaint>, IComplaintWriteRepository
    {
        public ComplaintWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
