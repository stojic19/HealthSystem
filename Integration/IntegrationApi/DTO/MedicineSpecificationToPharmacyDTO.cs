using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineSpecificationToPharmacyDTO
    {
        public Guid ApiKey { get; set; }
        public string MedicineName { get; set; }
    }
}
