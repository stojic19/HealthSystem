using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Model
{
    public class MedicineInventory
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
    }
}
