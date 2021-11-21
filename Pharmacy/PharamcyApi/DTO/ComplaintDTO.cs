using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class ComplaintDTO : BaseCommunicationDTO
    {
        [Required(ErrorMessage = "It is necessary to specify the time of creation!")]
        public DateTime CreatedDateTime { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the title of the complaint!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the content of the complaint!")]
        public string Description { get; set; }
    }
}
