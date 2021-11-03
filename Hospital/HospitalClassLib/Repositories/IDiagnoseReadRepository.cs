using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IDiagnoseReadRepository : IReadBaseRepository<int, Diagnose>
    {
    }
}
