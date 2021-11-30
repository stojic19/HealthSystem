using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class InventoryItemReadRepository : ReadBaseRepository<int, InventoryItem>, IInventoryItemReadRepository
    {
        public InventoryItemReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
