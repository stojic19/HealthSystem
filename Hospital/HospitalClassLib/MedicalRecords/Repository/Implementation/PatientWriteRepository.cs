using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class PatientWriteRepository : WriteBaseRepository<Patient>, IPatientWriteRepository
    {
        public PatientWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
