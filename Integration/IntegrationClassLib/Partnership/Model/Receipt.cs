using System;
using Integration.Shared.Model;

namespace Integration.Partnership.Model
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
