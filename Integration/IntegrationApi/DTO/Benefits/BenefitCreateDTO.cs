using System;

namespace IntegrationAPI.DTO.Benefits
{
    public class BenefitCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PharmacyName { get; set; }
    }
}
