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
        public static MedicineInformationRequestForPharmacyDTO CreateMedicineRequestToMedicineInformationRequest(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            MedicineInformationRequestForPharmacyDTO medicineRequestDTO = new MedicineInformationRequestForPharmacyDTO();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
        public static EmergencyProcurementRequestForPharmacyDTO CreateMedicineRequestToEmergencyProcurementRequest(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            EmergencyProcurementRequestForPharmacyDTO medicineRequestDTO = new EmergencyProcurementRequestForPharmacyDTO();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
    }
}
