using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AllergyReadRepository : ReadBaseRepository<int, Allergy>, IAllergyReadRepository
    {
        public AllergyReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
