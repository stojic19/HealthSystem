using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineConsumptionReportDTO
    {
        public string ApiKey;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createdDate { get; set; }
        //public IEnumerable<MedicineConsumption> MedicineConsumptions { get; set; }
    }
}
