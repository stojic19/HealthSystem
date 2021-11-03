using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IHospitalTreatmentReadRepository : IReadBaseRepository<int, HospitalTreatment>
    {
    }
}
