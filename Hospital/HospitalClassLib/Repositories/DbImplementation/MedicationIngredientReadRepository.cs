using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class MedicationIngredientReadRepository : ReadBaseRepository<int, MedicationIngredient>, IMedicationIngredientReadRepository
    {
        public MedicationIngredientReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
