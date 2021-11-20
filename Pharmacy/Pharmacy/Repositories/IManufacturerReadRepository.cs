using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories
{
    public interface IManufacturerReadRepository : IReadBaseRepository<int, Manufacturer>
    {
        public Manufacturer GetManufacturerByName(string name);
    }
}
