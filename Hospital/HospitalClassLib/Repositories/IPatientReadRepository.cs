using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IPatientReadRepository : IReadBaseRepository<int, Patient>
    {
    }
}
