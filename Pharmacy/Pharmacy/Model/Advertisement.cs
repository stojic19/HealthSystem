using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Model
{
    public class Advertisement
    {
        public int  Id{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }  
    }
}
