using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;
using System.Linq;

namespace Pharmacy.Repositories.DbImplementation
{
    public class MedicineReadRepository : ReadBaseRepository<int, Medicine>, IMedicineReadRepository
    {
        public MedicineReadRepository(AppDbContext context) : base(context)
        {
        }

        public Medicine GetMedicineByName(string name)
        {
            return GetAll()
                .FirstOrDefault(x => x.Name == name);
        }

        public Medicine GetMedicineByNameAndManufacturerName(string name, string manufacturerName)
        {
            return GetAll()
                .FirstOrDefault(medicine => medicine.Name == name && medicine.Manufacturer.Name.Equals(manufacturerName));
        }
    }
}
