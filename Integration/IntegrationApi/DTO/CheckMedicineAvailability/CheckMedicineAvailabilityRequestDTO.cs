using System;

namespace IntegrationAPI.DTO
{
    public class CheckMedicineAvailabilityRequestDto
    {
        public Guid ApiKey { get; set; }
        public String MedicineName { get; set; }
        public String ManufacturerName { get; set; }
        public int Quantity { get; set; }
    }
}
