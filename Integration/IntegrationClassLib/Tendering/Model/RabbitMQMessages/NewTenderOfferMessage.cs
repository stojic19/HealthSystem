using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Tendering.Model.RabbitMQMessages
{
    public class NewTenderOfferMessage
    {
        public Guid Apikey { get; set; }
        public List<MedicationRequestMessage> MedicationRequests { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TenderCreatedDate { get; set; }
        public double Cost { get; set; }
        public Currency Currency { get; set; }
    }
}
