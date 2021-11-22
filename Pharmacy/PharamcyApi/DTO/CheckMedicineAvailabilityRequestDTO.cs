using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.DTO
{
    public class CheckMedicineAvailabilityRequestDTO
    {
        public Guid ApiKey { get; set; }
        public string MedicineName { get; set; }
        public string ManufacturerName { get; set; }
        public int Quantity { get; set; }
    }
}
