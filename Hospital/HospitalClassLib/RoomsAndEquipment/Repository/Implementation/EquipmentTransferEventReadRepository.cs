using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;

namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class EquipmentTransferEventReadRepository : ReadBaseRepository<int, EquipmentTransferEvent>, IEquipmentTransferEventReadRepository
    {
        public EquipmentTransferEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
