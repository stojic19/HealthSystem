using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Database.EfStructures;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MedicalRecords.Repository.Implementation
{
    public class MedicationExpenditureLogReadRepository : ReadBaseRepository<int, MedicationExpenditureLog>, IMedicationExpenditureLogReadRepository 
    {
        public MedicationExpenditureLogReadRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<MedicationExpenditureLog> GetMedicationExpenditureLogsInTimePeriod(TimePeriod timePeriod)
        {
            List<MedicationExpenditureLog> medicationsExpenditureLogsInTimePeriod = new List<MedicationExpenditureLog>();
            IEnumerable<MedicationExpenditureLog> medicationsExpenditureLogs = GetAll().Include(x => x.Medication);
            foreach (var medicationExpenditureLog in medicationsExpenditureLogs)
                if (timePeriod.StartTime < medicationExpenditureLog.Date && medicationExpenditureLog.Date < timePeriod.EndTime)
                    medicationsExpenditureLogsInTimePeriod.Add(medicationExpenditureLog);
            return medicationsExpenditureLogsInTimePeriod;
        }
    }
}
