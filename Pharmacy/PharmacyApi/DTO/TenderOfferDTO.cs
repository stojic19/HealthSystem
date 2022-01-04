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
        public List<MedicationRequestDTO> MedicationRequestDtos { get; set; }
        public MoneyDto MoneyDto { get; set; }
        public int TenderId { get; set; }
    }
}
