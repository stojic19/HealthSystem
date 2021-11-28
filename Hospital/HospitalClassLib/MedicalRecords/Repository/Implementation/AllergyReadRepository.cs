using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class AllergyReadRepository : ReadBaseRepository<int, Allergy>, IAllergyReadRepository
    {
        public AllergyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
