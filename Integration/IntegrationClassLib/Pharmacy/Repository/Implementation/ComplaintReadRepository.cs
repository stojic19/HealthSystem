using Integration.Database.EfStructures;
using Integration.Pharmacy.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacy.Repository.Implementation
{
    public class ComplaintReadRepository : ReadBaseRepository<int, Complaint>, IComplaintReadRepository
    {
        public ComplaintReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
