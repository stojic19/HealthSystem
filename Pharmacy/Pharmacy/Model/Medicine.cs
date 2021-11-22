using System.Collections.Generic;

namespace Pharmacy.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<SideEffect> SideEffects { get; set; }
        public List<Reaction> Reactions { get; set; }
        public string Usage { get; set; }
        public double WeightInMilligrams { get; set; }
        public List<Precaution> Precautions { get; set; }
        public List<MedicinePotentialDanger> MedicinePotentialDangers { get; set; }
        public List<Substance> Substances { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

    }
}
