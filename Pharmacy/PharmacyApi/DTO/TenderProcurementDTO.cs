using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class TenderProcurementDTO : BaseCommunicationDTO
    {
        public List<TenderMedicineDTO> Medications { get; set; }
    }
}
