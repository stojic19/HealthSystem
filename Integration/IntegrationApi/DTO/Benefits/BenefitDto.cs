using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.DTO.Benefits
{
    public class BenefitDto
    {
        public string PharmacyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Picture { get; set; }
        public int PharmacyId { get; set; }
    }
}
