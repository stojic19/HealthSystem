using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class MedicineConsumptionDTO
    {
        [Required(ErrorMessage = "Medicine name is necessary!")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Amount of spent medicine is necessary!")]
        public int Amount { get; set; }
    }
}
