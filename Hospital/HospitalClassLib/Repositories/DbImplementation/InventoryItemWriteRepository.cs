using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class InventoryItemWriteRepository : WriteBaseRepository<InventoryItem>, IInventoryItemWriteRepository
    {
        public InventoryItemWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
