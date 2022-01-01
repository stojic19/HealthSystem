using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.Tender
{
    public class WinningOfferToPharmacyDto
    {
        public Guid ApiKey { get; set; }
        public DateTime TenderCreatedDate { get; set; }
        public DateTime TenderOfferCreatedDate { get; set; }
        public DateTime TenderClosedDate { get; set; }
    }
}
