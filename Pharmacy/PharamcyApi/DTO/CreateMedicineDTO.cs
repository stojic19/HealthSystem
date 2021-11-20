using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class CreateMedicineDTO
    {
        [Required(ErrorMessage = "It is necessary to specify the name of medicine!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the name of manufacturer!")]
        public string ManufacturerName { get; set; }
        public List<string> SideEffects { get; set; }
        public List<string> Reactions { get; set; }
        public string Usage { get; set; }
        public List<string> MedicinesThatCanBeCombined { get; set; }
        public double WeightInMilligrams { get; set; }
        public List<string> MainPrecautions { get; set; }
        public List<string> PotentialDangers { get; set; }
        public List<string> Substances { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the type of medicine!")]
        public string Type { get; set; }
        public int Quantity { get; set; }
    }
}
