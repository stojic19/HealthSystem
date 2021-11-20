using Integration.Model;
using IntegrationAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Adapters
{
    public class MedicineInventoryAdapter
    {
        public static MedicineRequestForPharmacyDTO CreateMedicineRequestToMedicineRequest(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            MedicineRequestForPharmacyDTO medicineRequestDTO = new MedicineRequestForPharmacyDTO();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
    }
}
