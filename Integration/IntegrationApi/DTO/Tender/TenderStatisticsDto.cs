using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.DTO.Tender
{
    public class TenderStatisticsDto
    {
        public List<PharmacyTenderStatisticsDto> PharmacyStatistics { get; set; }
    }
}
