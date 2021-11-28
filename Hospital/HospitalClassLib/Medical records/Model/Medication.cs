using System.Collections.Generic;

namespace Hospital.Medical_records.Model
{
    public class Medication 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TimesPerDay { get; set; }
        public string HowToUse { get; set; }

        public IEnumerable<MedicationIngredient> MedicationIngredients { get; set; }
    }
}
