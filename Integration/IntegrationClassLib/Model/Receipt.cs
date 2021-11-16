using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Model
{
    public class Receipt
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int AmountSpent { get; set; }

    }
}
