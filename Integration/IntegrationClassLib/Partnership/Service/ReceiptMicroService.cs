using Integration.Partnership.Model;
using Integration.Shared.Model;
using System.Collections.Generic;

namespace Integration.Partnership.Service
{
    public class ReceiptMicroService
    {
        public IEnumerable<Receipt> GetReceiptLogsInTimeRange(TimeRange timeRange, IEnumerable<Receipt> receiptLogs)
        {
            List<Receipt> receiptLogsInTimeRange = new List<Receipt>();
            foreach (var receiptLog in receiptLogs)
                if (timeRange.startDate < receiptLog.ReceiptDate && receiptLog.ReceiptDate < timeRange.endDate)
                    receiptLogsInTimeRange.Add(receiptLog);
            return receiptLogsInTimeRange;
        }

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
