using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class AllergyWriteRepository : WriteBaseRepository<Allergy>, IAllergyWriteRepository
    {
        public AllergyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
