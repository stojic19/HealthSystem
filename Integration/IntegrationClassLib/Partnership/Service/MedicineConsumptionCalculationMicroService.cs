using System.Collections.Generic;
using Integration.Partnership.Model;

namespace Integration.Partnership.Service
{
    public class MedicineConsumptionCalculationMicroService
    {
        public IEnumerable<MedicineConsumption> CalculateMedicineConsumptions(IEnumerable<Receipt> allLogs)
        {
            List<MedicineConsumption> medicineConsumptions = new List<MedicineConsumption>();
            foreach (var receiptLog in allLogs)
            {
                var medicationAlreadyInList = false;
                foreach (var medicineConsumption in medicineConsumptions)
                    if (medicineConsumption.Medicine.Name.Equals(receiptLog.Medicine.Name))
                    {
                        medicineConsumption.Amount += receiptLog.AmountSpent;
                        medicationAlreadyInList = true;
                        break;
                    }

                if (medicationAlreadyInList) continue;
                medicineConsumptions.Add(new MedicineConsumption
                {
                    Medicine = receiptLog.Medicine,
                    Amount = receiptLog.AmountSpent
                });
            }
            return medicineConsumptions;
        }
    }
}
