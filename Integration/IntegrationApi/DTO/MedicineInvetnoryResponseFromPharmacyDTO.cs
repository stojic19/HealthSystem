using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineInvetnoryResponseFromPharmacyDTO
    {
        public MedicineRequestForPharmacyDTO request { get; set; }
        public bool answer{ get; set; }
    }
}
