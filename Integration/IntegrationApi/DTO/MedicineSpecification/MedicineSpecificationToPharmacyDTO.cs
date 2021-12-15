using System;

namespace IntegrationAPI.DTO.MedicineSpecification
{
    public class MedicineSpecificationToPharmacyDTO
    {
        public Guid ApiKey { get; set; }
        public string MedicineName { get; set; }
    }
}
