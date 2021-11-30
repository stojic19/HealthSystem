using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository.Implementation
{
    public class BenefitWriteRepository : WriteBaseRepository<Benefit>, IBenefitWriteRepository
    {
        public BenefitWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
