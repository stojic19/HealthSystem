using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IRoomReadRepository : IReadBaseRepository<int, Room>
    {
    }
}
