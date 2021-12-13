using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class ApplyTenderOfferDTO
    {
        
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationTime { get; set; }
        public double TotalPrice { get; set; }
        public Guid ApiKey { get; set; }

    }
}
