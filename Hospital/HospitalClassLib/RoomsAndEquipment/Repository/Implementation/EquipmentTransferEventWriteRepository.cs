using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;


namespace Hospital.RoomsAndEquipment.Repository.Implementation
{
    public class EquipmentTransferEventWriteRepository : WriteBaseRepository<EquipmentTransferEvent>, IEquipmentTransferEventWriteRepository
    {
        public EquipmentTransferEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
