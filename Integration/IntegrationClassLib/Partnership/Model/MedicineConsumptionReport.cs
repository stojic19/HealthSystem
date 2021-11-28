using System;
using System.Collections.Generic;

namespace Integration.Partnership.Model
{
    public class MedicineConsumptionReport
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createdDate { get; set; }
        public IEnumerable<MedicineConsumption> MedicineConsumptions { get; set; }
    }
}
