using System;
using System.Collections.Generic;

namespace Hospital.MedicalRecords.Model
{
    public class MedicationConsumptionReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<MedicationConsumption> MedicationConsumptions { get; set; }
    }
}
