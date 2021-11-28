using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationIngredientReadRepository : ReadBaseRepository<int, MedicationIngredient>, IMedicationIngredientReadRepository
    {
        public MedicationIngredientReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
