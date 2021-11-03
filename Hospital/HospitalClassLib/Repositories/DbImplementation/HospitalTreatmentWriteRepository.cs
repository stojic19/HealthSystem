using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class HospitalTreatmentWriteRepository : WriteBaseRepository<HospitalTreatment>, IHospitalTreatmentWriteRepository
    {
        public HospitalTreatmentWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
