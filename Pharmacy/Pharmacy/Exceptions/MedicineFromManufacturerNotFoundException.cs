using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Exceptions
{
    public class MedicineFromManufacturerNotFoundException : Exception
    {
        public MedicineFromManufacturerNotFoundException() : base("There is no medicine matching given name and manufacturer name.")
        {

        }
    }
}
