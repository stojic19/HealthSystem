using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationReadRepository : ReadBaseRepository<int, Medication>, IMedicationReadRepository
    {
        public MedicationReadRepository(AppDbContext context) : base(context)
        {
        }

        public Medication GetMedicationByName(string name)
        {
            DbSet<Medication> allMedication = GetAll();
            Medication medicine = allMedication.FirstOrDefault(tempMedicine => tempMedicine.Name.Equals(name));
            return medicine;
        }
    }
}
