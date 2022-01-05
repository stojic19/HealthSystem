using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.Tender
{
    public class CreateTenderDto
    {
        [Required(ErrorMessage = "Tender name is necessary!")]
        public string Name{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<MedicineRequestDto> MedicineRequests { get; set; }
    }
}
