using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class InventoryItemWriteRepository : WriteBaseRepository<InventoryItem>, IInventoryItemWriteRepository
    {
        public InventoryItemWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
