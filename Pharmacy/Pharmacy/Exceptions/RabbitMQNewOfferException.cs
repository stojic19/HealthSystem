using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Exceptions
{
    public class RabbitMQNewOfferException : Exception
    {
        public RabbitMQNewOfferException() : base("Error while sending new offer via rabbitmq") { }
    }
}
