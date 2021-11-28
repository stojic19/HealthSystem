using Integration.Database.EfStructures;
using Integration.Pharmacy.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacy.Repository.Implementation
{
    public class ComplaintWriteRepository : WriteBaseRepository<Complaint>, IComplaintWriteRepository
    {
        public ComplaintWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
