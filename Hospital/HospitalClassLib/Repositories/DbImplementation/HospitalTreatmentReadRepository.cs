using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class HospitalTreatmentReadRepository : ReadBaseRepository<int, HospitalTreatment>, IHospitalTreatmentReadRepository
    {
        public HospitalTreatmentReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
