using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class ConsumptionReportDTO : BaseCommunicationDTO
    {
        [Required(ErrorMessage = "It is necessary to specify the hospital ApiKey")]
        public Guid ApiKey { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the file name of sent report")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the network adress of sftp host")]
        public string Host { get; set; }
    }
}
