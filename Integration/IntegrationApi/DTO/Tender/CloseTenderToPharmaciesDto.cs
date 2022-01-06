using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace IntegrationAPI.DTO.Tender
{
    public class CloseTenderToPharmaciesDto
    {
        public Guid ApiKey { get; set; }
        public DateTime TenderCreatedDate { get; set; }
        public DateTime TenderClosedDate { get; set; }
    }
}
