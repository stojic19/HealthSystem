using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ComplaintReadRepository : ReadBaseRepository<int, Complaint>, IComplaintReadRepository
    {
        public ComplaintReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
