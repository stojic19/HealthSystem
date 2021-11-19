using System.Collections.Generic;

namespace Pharmacy.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<string> SideEffects { get; set; }
        public List<string> Reactions { get; set; }
        public string Usage { get; set; }
        public List<string> MedicinesThatCanBeCombined { get; set; }
        public double WeightInMilligrams { get; set; }
        public List<string> MainPrecautions { get; set; }
        public List<string> PotentialDangers { get; set; }
        public List<string> Substances { get; set; }
        public string Type { get; set; }

    }
}
