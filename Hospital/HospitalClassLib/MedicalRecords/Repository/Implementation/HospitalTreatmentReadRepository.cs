using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class HospitalTreatmentReadRepository : ReadBaseRepository<int, HospitalTreatment>, IHospitalTreatmentReadRepository
    {
        public HospitalTreatmentReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
