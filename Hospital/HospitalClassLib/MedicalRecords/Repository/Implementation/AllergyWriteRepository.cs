using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class AllergyWriteRepository : WriteBaseRepository<Allergy>, IAllergyWriteRepository
    {
        public AllergyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
