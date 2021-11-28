using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.SharedModel.Repository
{
    public interface IStaffReadRepository : IReadBaseRepository<int, Staff>
    {
    }
}
