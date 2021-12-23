using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class TenderOffer
    {
        public int Id { get; set; }
        public int MedicineId  { get; set; }
        public  Medicine Medicine { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int Quantity  { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
