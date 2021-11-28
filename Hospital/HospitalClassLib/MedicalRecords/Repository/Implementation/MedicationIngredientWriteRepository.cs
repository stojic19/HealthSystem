using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationIngredientWriteRepository : WriteBaseRepository<MedicationIngredient>, IMedicationIngredientWriteRepository
    {
        public MedicationIngredientWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
