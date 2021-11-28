using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository.Implementation
{
    public class MedicineSpecificationWriteRepository : WriteBaseRepository<MedicineSpecificationFile>, IMedicineSpecificationFileWriteRepository
    {
        public MedicineSpecificationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
