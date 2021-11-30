using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class HospitalTreatmentWriteRepository : WriteBaseRepository<HospitalTreatment>, IHospitalTreatmentWriteRepository
    {
        public HospitalTreatmentWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
