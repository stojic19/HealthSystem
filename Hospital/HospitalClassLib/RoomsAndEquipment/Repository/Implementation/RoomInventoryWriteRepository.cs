using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class RoomInventoryWriteRepository : WriteBaseRepository<RoomInventory>, IRoomInventoryWriteRepository
    {
        public RoomInventoryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
