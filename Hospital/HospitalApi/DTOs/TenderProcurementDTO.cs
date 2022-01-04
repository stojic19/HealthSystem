using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class TenderProcurementDTO
    {
        public Guid ApiKey { get; set; }
        public List<TenderMedicineDTO> Medications { get; set; }
    }
}
