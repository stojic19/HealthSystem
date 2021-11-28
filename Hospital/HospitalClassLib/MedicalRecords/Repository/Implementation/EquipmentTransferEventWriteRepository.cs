using Hospital.Database.EfStructures;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class EquipmentTransferEventWriteRepository : WriteBaseRepository<EquipmentTransferEvent>, IEquipmentTransferEventWriteRepository
    {
        public EquipmentTransferEventWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
