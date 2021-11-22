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
        public static CheckMedicineAvailabilityRequestDTO CreateMedicineRequestToMedicineInformationRequest(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            CheckMedicineAvailabilityRequestDTO medicineRequestDTO = new CheckMedicineAvailabilityRequestDTO();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.ManufacturerName = createMedicineRequestDTO.ManufacturerName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
        public static MedicineProcurementRequestDTO CreateMedicineRequestToEmergencyProcurementRequest(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            MedicineProcurementRequestDTO medicineRequestDTO = new MedicineProcurementRequestDTO();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.ManufacturerName = createMedicineRequestDTO.ManufacturerName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
    }
}
