using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IStaffReadRepository : IReadBaseRepository<int, Staff>
    {
    }
}
