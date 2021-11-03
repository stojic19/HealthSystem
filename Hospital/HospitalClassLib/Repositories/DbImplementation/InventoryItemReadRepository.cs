using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class InventoryItemReadRepository : ReadBaseRepository<int, InventoryItem>, IInventoryItemReadRepository
    {
        public InventoryItemReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
