using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class ReportDTO
    {
        public Guid ApiKey { get; set; }
        public string FileName { get; set; }
        public string Host { get; set; }
    }
}
