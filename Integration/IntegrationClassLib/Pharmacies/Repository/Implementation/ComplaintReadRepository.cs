using Integration.Database.EfStructures;
using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class ComplaintReadRepository : ReadBaseRepository<int, Complaint>, IComplaintReadRepository
    {
        public ComplaintReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
