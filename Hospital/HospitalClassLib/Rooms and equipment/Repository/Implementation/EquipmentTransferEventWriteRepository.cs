using Hospital.Database.EfStructures;
using Hospital.Rooms_and_equipment.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Rooms_and_equipment.Repository.Implementation
{
    public class EquipmentTransferEventWriteRepository : WriteBaseRepository<EquipmentTransferEvent>, IEquipmentTransferEventWriteRepository
    {
        public EquipmentTransferEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
