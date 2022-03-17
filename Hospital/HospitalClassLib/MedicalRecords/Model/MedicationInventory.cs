namespace Hospital.MedicalRecords.Model
{
    public class MedicationInventory
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public int Quantity { get; set; }
    }
}
