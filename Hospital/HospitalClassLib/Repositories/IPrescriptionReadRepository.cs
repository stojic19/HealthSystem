using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IPrescriptionReadRepository : IReadBaseRepository<int, Prescription>
    {
    }
}
