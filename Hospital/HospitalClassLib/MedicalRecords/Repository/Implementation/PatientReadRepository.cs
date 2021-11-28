using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class PatientReadRepository : ReadBaseRepository<int, Patient>, IPatientReadRepository
    {
        public PatientReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
