using Integration.Database.EfStructures;
using Integration.Pharmacy.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacy.Repository.Implementation
{
    public class ComplaintResponseWriteRepository : WriteBaseRepository<ComplaintResponse>, IComplaintResponseWriteRepository
    {
        public ComplaintResponseWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
