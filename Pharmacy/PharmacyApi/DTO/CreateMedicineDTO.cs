using PharmacyApi.DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class CreateMedicineDTO : BaseCommunicationDTO
    {
        [Required(ErrorMessage = "It is necessary to specify the name of medicine!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the name of manufacturer!")]
        public string ManufacturerName { get; set; }
        public string SideEffects { get; set; }
        public string Reactions { get; set; }
        public string Usage { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public double WeightInMilligrams { get; set; }
        public string Precautions { get; set; }
        public string MedicinePotentialDangers { get; set; }
        public List<string> Substances { get; set; }
        [Required(ErrorMessage = "It is necessary to specify the type of medicine!")]
        public string Type { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Quantity { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public double Price { get; set; }
    }
}
