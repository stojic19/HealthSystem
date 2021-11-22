using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class MedicineProcurement
    {
        public int HospitalId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
