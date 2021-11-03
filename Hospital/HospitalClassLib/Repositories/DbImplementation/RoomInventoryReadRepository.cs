using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class RoomInventoryReadRepository : ReadBaseRepository<int, RoomInventory>, IRoomInventoryReadRepository
    {
        public RoomInventoryReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
