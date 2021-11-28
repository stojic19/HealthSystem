using Hospital.Database.EfStructures;
using Hospital.Medical_records.Model;
using Hospital.Shared_model.Repository.Base;

namespace Hospital.Medical_records.Repository.Implementation
{
    public class AllergyReadRepository : ReadBaseRepository<int, Allergy>, IAllergyReadRepository
    {
        public AllergyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
