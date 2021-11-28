using Integration.Shared.Model;

namespace Integration.Partnership.Model
{
    public class MedicineInventory
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
    }
}
