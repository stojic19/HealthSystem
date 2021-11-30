using System.Linq;
using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Partnership.Repository.Implementation
{
    class MedicineInventoryReadRepository : ReadBaseRepository<int, MedicineInventory>, IMedicineInventoryReadRepository
    {
        public MedicineInventoryReadRepository(AppDbContext context) : base(context)
        {
        }
        public DbSet<MedicineInventory> GetAllMedicines()
        {
            return GetAll();
        }

        public MedicineInventory GetMedicineByMedicineId(int id)
        {
            DbSet<MedicineInventory> inventory = GetAll();
            MedicineInventory medicine = inventory.FirstOrDefault(tempMedicine => tempMedicine.MedicineId.Equals(id));
            return medicine;
        }
    }
}
