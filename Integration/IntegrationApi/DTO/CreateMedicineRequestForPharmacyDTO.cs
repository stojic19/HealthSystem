using System;

namespace IntegrationAPI.DTO
{
    public class CreateMedicineRequestForPharmacyDTO
    {
        public int PharmacyId { get; set; }
        public String MedicineName { get; set; }
        public String ManufacturerName { get; set; }
        public int Quantity { get; set; }
        public bool GrpcCommunication { get; set; }
    }
}
