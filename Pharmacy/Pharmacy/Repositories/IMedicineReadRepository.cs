using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories
{
    public interface IMedicineReadRepository : IReadBaseRepository<int, Medicine>
    {
        public Medicine GetMedicineByName(string name);
        public Medicine GetMedicineByNameAndManufacturerName(string name, string manufacturerName);
    }
}
