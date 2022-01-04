using PharmacyApi.DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class TenderOfferDTO : BaseCommunicationDTO
    {
        [Required(ErrorMessage = "It is necessary to specify the  medicine name!")]
        public string MedicineName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the creation time!")]
        public DateTime CreationTime { get; set; }
    }
}
