using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationInventoryReadRepository : ReadBaseRepository<int, MedicationInventory>, IMedicationInventoryReadRepository
    {
        public MedicationInventoryReadRepository(AppDbContext context) : base(context)
        {
        }
        public MedicationInventory GetMedicationByMedicationId(int id)
        {
            DbSet<MedicationInventory> inventory = GetAll();
            MedicationInventory medication = inventory.FirstOrDefault(tempMedication => tempMedication.MedicationId.Equals(id));
            return medication;
        }
    }
}
