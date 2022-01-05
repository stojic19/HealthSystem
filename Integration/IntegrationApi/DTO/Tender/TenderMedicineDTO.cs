using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO.Tender
{
    public class TenderMedicineDto
    {
        public MedicineDto Medicine { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Quantity { get; set; }
    }
}
