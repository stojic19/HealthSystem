using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO.MedicineDTO
{
    public class MedicineDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public List<string> SideEffects { get; set; }
        public List<string> Reactions { get; set; }
        public List<string> Precautions { get; set; }
        public List<string> MedicinePotentialDangers { get; set; }
        public List<string> Substances { get; set; }
        public string Type { get; set; }
        public string Usage { get; set; }
        public double WeightInMilligrams { get; set; }
    }
}
