using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO.MedicineSpecification
{
    public class MedicineSpecificationRequestDTO
    {
        [Required(ErrorMessage = "Medicine Name is required!")]
        public string MedicineName { get; set; }
        [Required(ErrorMessage = "Pharmacy Id is required!")]
        public int PharmacyId { get; set; }
    }
}
