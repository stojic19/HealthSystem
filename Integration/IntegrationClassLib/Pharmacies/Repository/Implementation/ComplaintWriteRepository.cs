using Integration.Database.EfStructures;
using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class ComplaintWriteRepository : WriteBaseRepository<Complaint>, IComplaintWriteRepository
    {
        public ComplaintWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
