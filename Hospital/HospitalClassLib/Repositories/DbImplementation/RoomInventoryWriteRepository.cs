using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class RoomInventoryWriteRepository : WriteBaseRepository<RoomInventory>, IRoomInventoryWriteRepository
    {
        public RoomInventoryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
