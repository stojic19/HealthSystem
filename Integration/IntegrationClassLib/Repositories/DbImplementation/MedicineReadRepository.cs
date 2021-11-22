using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories.DbImplementation
{
    class MedicineReadRepository : ReadBaseRepository<int, Medicine>, IMedicineReadRepository
    {
        public MedicineReadRepository(AppDbContext context) : base(context)
        {
        }
        public DbSet<Medicine> GetAllMedicines()
        {
            return GetAll();
        }

        public Medicine GetMedicineByName(string name)
        {
            DbSet<Medicine> allMedicine = GetAll();
            Medicine medicine = allMedicine.FirstOrDefault(tempMedicine => tempMedicine.Name.Equals(name));
            return medicine;
        }
    }
}
