using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class CheckMedicineAvailabilityRequestDTO
    {
        [Required(ErrorMessage = "It is necessary to provide the hospital API key.")]
        public Guid ApiKey { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the medicine name.")]
        public string MedicineName { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the manufacturer name.")]
        public string ManufacturerName { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed for quantity.")]
        public int Quantity { get; set; }
    }
}
