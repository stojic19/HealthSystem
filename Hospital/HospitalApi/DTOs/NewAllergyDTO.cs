namespace HospitalApi.DTOs
{
    public class NewAllergyDTO
    {
        public int MedicalRecordId { get; set; }
        public int MedicalIngredientId { get; set; }
        public MedicationIngredientDTO MedicationIngredient { get; set; }

    }
}