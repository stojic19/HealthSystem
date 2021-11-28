using Hospital.Shared_model.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Shared_model.Repository
{
    public interface IDoctorReadRepository : IReadBaseRepository<int, Doctor>
    {
    }
}
