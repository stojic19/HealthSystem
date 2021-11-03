using Hospital.Model;
using Hospital.Repositories.Base;

namespace Hospital.Repositories
{
    public interface IMedicationIngredientReadRepository : IReadBaseRepository<int, MedicationIngredient>
    {
    }
}
