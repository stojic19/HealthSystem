using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.MedicineConsumption
{
    public class MedicineConsumptionReportToPdfDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<MedicationExpenditureDTO> MedicineConsumptions { get; set; }
        public string HospitalName { get; set; }
    }
}
