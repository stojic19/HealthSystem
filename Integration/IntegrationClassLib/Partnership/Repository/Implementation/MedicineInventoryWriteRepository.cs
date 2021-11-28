using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository.Implementation
{
    public class MedicineInventoryWriteRepository : WriteBaseRepository<MedicineInventory>, IMedicineInventoryWriteRepository
    {
        public MedicineInventoryWriteRepository(AppDbContext context) : base(context)
        {

        }
    }
}
