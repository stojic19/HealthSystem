using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationExpenditureLogWriteRepository : WriteBaseRepository<MedicationExpenditureLog>, IMedicationExpenditureLogWriteRepository
    {
        public MedicationExpenditureLogWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
