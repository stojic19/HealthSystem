using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IInventoryItemReadRepository : IReadBaseRepository<int, InventoryItem>
    {
    }
}
