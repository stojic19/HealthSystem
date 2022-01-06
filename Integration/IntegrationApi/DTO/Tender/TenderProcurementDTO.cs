using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.Tender
{
    public class TenderProcurementDto
    {
        public Guid ApiKey { get; set; }
        public List<TenderMedicineDto> Medications { get; set; }
    }
}
