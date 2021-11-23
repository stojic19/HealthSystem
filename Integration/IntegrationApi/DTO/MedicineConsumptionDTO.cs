using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineConsumptionDTO
    {
        [Required(ErrorMessage = "Medicine name is necessary!")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Amount of spent medicine is necessary!")]
        public int Amount { get; set; }
    }
}
