using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Exceptions
{
    public class TenderNotFoundException : Exception
    {
        public TenderNotFoundException() : base("Tender offer with given id doens't exist!")
        {

        }
    }
}
