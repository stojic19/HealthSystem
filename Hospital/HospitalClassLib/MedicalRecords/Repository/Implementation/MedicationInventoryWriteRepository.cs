using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    class MedicationInventoryWriteRepository : WriteBaseRepository<MedicationInventory>, IMedicationInventoryWriteRepository
    {
        public MedicationInventoryWriteRepository(AppDbContext context) : base(context)
        {

        }
    }
}
