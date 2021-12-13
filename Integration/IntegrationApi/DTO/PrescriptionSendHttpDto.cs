using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class PrescriptionSendHttpDto
    {
        public byte[] FileContent { get; set; }
        public Guid ApiKey { get; set; }
        public string FileName { get; set; }

    }
}
