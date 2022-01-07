using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.Tender
{
    public class TenderToPharmacyDto
    {
        public Guid Apikey { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<MedicationRequestDto> MedicationRequestDto { get; set; }

    }
}
