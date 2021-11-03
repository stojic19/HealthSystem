using System.Collections.Generic;

namespace Hospital.Model
{
    public class MedicationIngredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Medication> Medications { get; set; }
    }
}
