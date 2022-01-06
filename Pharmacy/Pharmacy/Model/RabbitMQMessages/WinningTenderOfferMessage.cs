using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model.RabbitMQMessages
{
    public class WinningTenderOfferMessage
    {
        public Guid ApiKey { get; set; }
        public DateTime TenderCreatedDate { get; set; }
        public DateTime TenderOfferCreatedDate { get; set; }
        public DateTime TenderClosedDate { get; set; }
    }
}
