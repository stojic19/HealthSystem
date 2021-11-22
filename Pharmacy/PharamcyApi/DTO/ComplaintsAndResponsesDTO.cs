using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class ComplaintsAndResponsesDTO : BaseCommunicationDTO
    {
        public ComplaintDTO Complaint { get; set; }
        public ComplaintResponseDTO ComplaintResponse { get; set; }
    }
}
