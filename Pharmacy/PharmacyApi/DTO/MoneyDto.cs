using System.ComponentModel.DataAnnotations;
using Pharmacy.Model;

namespace PharmacyApi.DTO
{
    public class MoneyDto
    {
        [Range(1,double.MaxValue, ErrorMessage = "Amount must be positive!")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Currency is required!")]
        public Currency Currency { get; set; }

    }
}