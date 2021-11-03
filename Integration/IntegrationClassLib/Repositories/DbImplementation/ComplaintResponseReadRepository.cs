using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ComplaintResponseReadRepository : ReadBaseRepository<int, ComplaintResponse>, IComplaintResponseReadRepository
    {
        public ComplaintResponseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
