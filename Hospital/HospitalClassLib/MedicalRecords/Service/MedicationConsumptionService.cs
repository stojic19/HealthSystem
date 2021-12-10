using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;

namespace Hospital.MedicalRecords.Service
{
    public class MedicationConsumptionService
    {
        public IEnumerable<MedicationConsumption> CalculateMedicationConsumptions(
            IEnumerable<MedicationExpenditureLog> allLogs)
        {
            List<MedicationConsumption> medicationConsumptions = new List<MedicationConsumption>();
            foreach (var medicationExpenditureLog in allLogs)
            {
                var medicationAlreadyInList = false;
                foreach (var medicationConsumption in medicationConsumptions)
                    if (medicationConsumption.Medication.Name.Equals(medicationExpenditureLog.Medication.Name))
                    {
                        medicationConsumption.Amount += medicationExpenditureLog.AmountSpent;
                        medicationAlreadyInList = true;
                        break;
                    }

                if (medicationAlreadyInList) continue;
                medicationConsumptions.Add(new MedicationConsumption()
                {
                    Medication = medicationExpenditureLog.Medication,
                    Amount = medicationExpenditureLog.AmountSpent
                });
            }
            return medicationConsumptions;
        }
    }
}
