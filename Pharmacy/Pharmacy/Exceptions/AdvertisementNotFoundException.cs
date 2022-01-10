using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Exceptions
{
    public class AdvertisementNotFoundException : Exception
    {
        public AdvertisementNotFoundException(): base("Ad with given id not found!")
        {
        }
    }
}
