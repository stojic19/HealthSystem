using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository.Implementation
{
    public class MedicineSpecificationFileReadRepository : ReadBaseRepository<int, MedicineSpecificationFile>, IMedicineSpecificationFileReadRepository
    {
        public MedicineSpecificationFileReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
