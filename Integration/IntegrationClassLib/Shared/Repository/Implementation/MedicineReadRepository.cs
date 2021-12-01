using System.Linq;
using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Shared.Repository.Implementation
{
    public class MedicineReadRepository : ReadBaseRepository<int, Medicine>, IMedicineReadRepository
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
