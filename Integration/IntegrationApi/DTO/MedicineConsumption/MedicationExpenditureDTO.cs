using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO.MedicineConsumption
{
    public class MedicationExpenditureDTO
    {
        [Required(ErrorMessage = "Medicine name is necessary!")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Amount of spent medicine is necessary!")]
        public int Amount { get; set; }
    }
}
