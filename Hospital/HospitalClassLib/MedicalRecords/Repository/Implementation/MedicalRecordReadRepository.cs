using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicalRecordReadRepository : ReadBaseRepository<int, MedicalRecord>, IMedicalRecordReadRepository
    {
        public MedicalRecordReadRepository(AppDbContext context) : base(context)
        {
        }

        public MedicalRecord GetMedicalRecord(int id)
        {
            return GetAll().Include(x => x.Doctor).Include(x => x.Allergies).
                            ThenInclude(x => x.MedicationIngredient).First(x=>x.Id == id);
        }
    }
}
