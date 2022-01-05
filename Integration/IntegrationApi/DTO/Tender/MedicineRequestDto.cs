using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO.Tender
{
    public class MedicineRequestDto
    {
        [Required(ErrorMessage = "Medicine name is necessary!")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Medicine quantity is necessary!")]
        public int Quantity { get; set; }
    }
}
