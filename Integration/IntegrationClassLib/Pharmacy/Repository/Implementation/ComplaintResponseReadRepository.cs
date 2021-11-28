using Integration.Database.EfStructures;
using Integration.Pharmacy.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacy.Repository.Implementation
{
    public class ComplaintResponseReadRepository : ReadBaseRepository<int, ComplaintResponse>, IComplaintResponseReadRepository
    {
        public ComplaintResponseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
