using Pharmacy.Model;
using Pharmacy.Repositories.Base;

namespace Pharmacy.Repositories
{
    public interface IMedicineReadRepository : IReadBaseRepository<int, Medicine>
    {
    }
}
