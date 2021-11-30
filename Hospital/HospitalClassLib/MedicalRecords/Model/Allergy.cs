namespace Hospital.MedicalRecords.Model
{
    public class Allergy
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }

        public MedicationIngredient MedicationIngredient { get; set; }
        public int MedicalIngredientId { get; set; }
    }
}
