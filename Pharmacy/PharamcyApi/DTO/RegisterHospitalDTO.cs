using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class RegisterHospitalDTO
    {
        [Required(ErrorMessage = "Hospital name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Street name is required!")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Street number is required!")]
        public string StreetNumber { get; set; }
        [Required(ErrorMessage = "City is required!")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Base url is required for http communication!")]
        public string BaseUrl { get; set; }


    }
}
