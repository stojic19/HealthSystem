using Integration.Database.EfStructures;
using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class ComplaintResponseReadRepository : ReadBaseRepository<int, ComplaintResponse>, IComplaintResponseReadRepository
    {
        public ComplaintResponseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
