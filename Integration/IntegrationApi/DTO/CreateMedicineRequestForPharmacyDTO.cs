using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class CreateMedicineRequestForPharmacyDTO
    {
        public int PharmacyId { get; set; }
        public String MedicineName { get; set; }
        public String ManufacturerName { get; set; }
        public int Quantity { get; set; }
    }
}
