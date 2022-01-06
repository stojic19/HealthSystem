using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class MedicineDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SideEffects { get; set; }
        public string Reactions { get; set; }
        public string Precautions { get; set; }
        public string MedicinePotentialDangers { get; set; }
        public List<string> Substances { get; set; }
        public string Type { get; set; }
        public string Usage { get; set; }
        public double WeightInMilligrams { get; set; }
    }
}
