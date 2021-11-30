using System.Collections.Generic;

namespace Hospital.MedicalRecords.Model
{
    public class MedicationIngredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Medication> Medications { get; set; }
    }
}
