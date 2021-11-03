using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    public class ComplaintResponseWriteRepository : WriteBaseRepository<ComplaintResponse>, IComplaintResponseWriteRepository
    {
        public ComplaintResponseWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
