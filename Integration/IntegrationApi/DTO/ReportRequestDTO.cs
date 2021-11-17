using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Model;

namespace IntegrationAPI.DTO
{
    public class ReportRequestDTO
    {
        public int PharmacyId { get; set; }
        public MedicineConsumptionReport MedicineConsumptionReport { get; set; }
    }
}
