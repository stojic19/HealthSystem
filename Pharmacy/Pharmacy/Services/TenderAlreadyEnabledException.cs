using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class TenderAlreadyEnabledException : Exception
    {
        public TenderAlreadyEnabledException() : base("Tender has been already enabled!")
        {

        }
    }
}
