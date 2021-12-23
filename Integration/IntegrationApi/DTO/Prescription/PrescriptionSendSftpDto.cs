using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.Prescription
{
    public class PrescriptionSendSftpDto
    {
        public Guid ApiKey { get; set; }
        public string FileName { get; set; }
    }
}
