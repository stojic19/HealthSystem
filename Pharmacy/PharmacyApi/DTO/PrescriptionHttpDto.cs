using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class PrescriptionHttpDto : BaseCommunicationDTO
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }

    }
}
