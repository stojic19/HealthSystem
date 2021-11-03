using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IDoctorReadRepository : IReadBaseRepository<int, Doctor>
    {
    }
}
