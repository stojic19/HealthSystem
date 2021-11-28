using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO.Base;

namespace PharmacyApi.DTO
{
    public class SpecificationDTO : BaseCommunicationDTO
    {
        [Required(ErrorMessage = "Medicine Name is required!")]
        public string MedicineName { get; set; }
    }
}
