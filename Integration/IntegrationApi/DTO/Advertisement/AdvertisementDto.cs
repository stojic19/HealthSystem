using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.DTO.Advertisement
{
    public class AdvertisementDto
    {
        public string PharmacyName { get; set; }
        [Required (ErrorMessage = "Advertisement title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Medication name is required!")]
        public string MedicationName { get; set; }
    }
}
