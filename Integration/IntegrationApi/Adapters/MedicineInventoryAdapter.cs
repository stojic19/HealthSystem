using Integration.Pharmacies.Model;
using IntegrationAPI.DTO;

namespace IntegrationAPI.Adapters
{
    public class MedicineInventoryAdapter
    {
        public static CheckMedicineAvailabilityRequestDto CreateMedicineRequestToMedicineInformationRequest(CreateMedicineRequestForPharmacyDto createMedicineRequestDTO, Pharmacy pharmacy)
        {
            CheckMedicineAvailabilityRequestDto medicineRequestDTO = new CheckMedicineAvailabilityRequestDto();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.ManufacturerName = createMedicineRequestDTO.ManufacturerName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
        public static MedicineProcurementRequestDto CreateMedicineRequestToEmergencyProcurementRequest(CreateMedicineRequestForPharmacyDto createMedicineRequestDTO, Pharmacy pharmacy)
        {
            MedicineProcurementRequestDto medicineRequestDTO = new MedicineProcurementRequestDto();
            medicineRequestDTO.ApiKey = pharmacy.ApiKey;
            medicineRequestDTO.MedicineName = createMedicineRequestDTO.MedicineName;
            medicineRequestDTO.ManufacturerName = createMedicineRequestDTO.ManufacturerName;
            medicineRequestDTO.Quantity = createMedicineRequestDTO.Quantity;
            return medicineRequestDTO;
        }
    }
}
