using Hospital.Database.EfStructures;
using Hospital.Rooms_and_equipment.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Rooms_and_equipment.Repository.Implementation
{
    public class EquipmentTransferEventReadRepository : ReadBaseRepository<int, EquipmentTransferEvent>, IEquipmentTransferEventReadRepository
    {
        public EquipmentTransferEventReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
