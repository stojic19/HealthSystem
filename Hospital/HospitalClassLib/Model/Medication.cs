using System.Collections.Generic;

namespace Hospital.Model
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
