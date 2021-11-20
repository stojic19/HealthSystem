using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class CreateComplaintResponseDTO
    {
        [Required(ErrorMessage = "It is necessary to specify which complaint is being answered!")]
        public int ComplaintId { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the content of the answer!")]
        public string Description { get; set; }
    }
}
