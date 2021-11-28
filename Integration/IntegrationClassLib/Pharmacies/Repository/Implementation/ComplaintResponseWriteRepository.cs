using Integration.Database.EfStructures;
using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class ComplaintResponseWriteRepository : WriteBaseRepository<ComplaintResponse>, IComplaintResponseWriteRepository
    {
        public ComplaintResponseWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
