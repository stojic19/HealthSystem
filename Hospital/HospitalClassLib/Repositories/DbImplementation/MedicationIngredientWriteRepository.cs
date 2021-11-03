using Hospital.EfStructures;
using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories.DbImplementation
{
    public class MedicationIngredientWriteRepository : WriteBaseRepository<MedicationIngredient>, IMedicationIngredientWriteRepository
    {
        public MedicationIngredientWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
