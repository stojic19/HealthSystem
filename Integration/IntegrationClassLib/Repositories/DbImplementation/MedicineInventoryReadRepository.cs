using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Repositories.DbImplementation
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
            MedicineInventory medicine = inventory.FirstOrDefault(medicine => medicine.MedicineId.Equals(id));
            return medicine;
        }
    }
}
