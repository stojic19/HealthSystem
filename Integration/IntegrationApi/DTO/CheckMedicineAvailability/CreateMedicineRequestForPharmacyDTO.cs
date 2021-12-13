using System;

namespace IntegrationAPI.DTO
{
    public class CreateMedicineRequestForPharmacyDto
    {
        public int PharmacyId { get; set; }
        public String MedicineName { get; set; }
        public String ManufacturerName { get; set; }
        public int Quantity { get; set; }

    }
}
