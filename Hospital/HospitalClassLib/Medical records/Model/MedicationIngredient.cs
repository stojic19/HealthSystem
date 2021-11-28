using System.Collections.Generic;

namespace Hospital.Medical_records.Model
{
    public class MedicationIngredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Medication> Medications { get; set; }
    }
}
