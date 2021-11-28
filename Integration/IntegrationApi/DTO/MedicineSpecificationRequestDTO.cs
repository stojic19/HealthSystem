using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineSpecificationRequestDTO
    {
        [Required(ErrorMessage = "Medicine Name is required!")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Pharmacy Id is required!")]
        public int PharmacyId { get; set; }
    }
}
