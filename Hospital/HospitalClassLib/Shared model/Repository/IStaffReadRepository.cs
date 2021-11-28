using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository
{
    public interface IStaffReadRepository : IReadBaseRepository<int, Staff>
    {
    }
}
