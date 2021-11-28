using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository
{
    public interface IMedicineInventoryReadRepository : IReadBaseRepository<int, MedicineInventory>
    {
        public MedicineInventory GetMedicineByMedicineId(int id);
    }
}
