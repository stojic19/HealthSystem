using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class CreateAdvertisementDTO
    {
        public string Title { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the description of ad!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the medicine for which ad is created!")]
        public int MedicineId { get; set; }
    }
}
