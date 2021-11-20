using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class ComplaintResponseDTO : BaseCommunicationDTO
    {
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
