using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationApi.DTO.Shared;

namespace IntegrationApi.DTO.Tender
{
    public class PharmacyTenderStatisticsDto
    {
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int TendersEntered { get; set; }
        public int TendersWon { get; set; }
        public int TenderOffersMade { get; set; }
        public MoneyDto Profit { get; set; }
    }
}
