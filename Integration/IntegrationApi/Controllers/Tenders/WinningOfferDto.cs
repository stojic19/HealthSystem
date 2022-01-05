using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controllers.Tenders
{
    public class WinningOfferDto
    {
        [Required(ErrorMessage="Tender id is required")]
        public int TenderId { get; set; }
        [Required(ErrorMessage = "Tender offer id is required")]
        public int TenderOfferId { get; set; }
    }
}
