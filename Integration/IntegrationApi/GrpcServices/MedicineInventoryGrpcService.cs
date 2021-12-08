using Grpc.Core;
using Grpc.Net.Client;
using Integration.Pharmacies.Model;
using IntegrationAPI.DTO;
using IntegrationAPI.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.gRPCServices
{
    public class MedicineInventorygRPCService
    {
        public static CheckMedicineAvailabilityGrpcResponseDTO CheckMedicineAvailability(CreateMedicineRequestForPharmacyDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            try
            {
                var channel = new Channel(pharmacy.BaseUrl, ChannelCredentials.Insecure);
                var client = new MedicineInventoryService.MedicineInventoryServiceClient(channel);
                var input = new CheckMedicineAvailabilityRequestModel() { ApiKey = pharmacy.ApiKey.ToString(), ManufacturerName = medicineRequestDTO.ManufacturerName, MedicineName = medicineRequestDTO.MedicineName, Quantity = medicineRequestDTO.Quantity };
                var response = client.CheckMedicineAvailability(input);
                return new CheckMedicineAvailabilityGrpcResponseDTO { ConnectionSuccesfull = true, ExceptionMessage = response.ExceptionMessage, Response = new CheckMedicineAvailabilityResponseDTO { Answer = response.Answer } };
            }
            catch (Exception exc)
            {
                return new CheckMedicineAvailabilityGrpcResponseDTO { ConnectionSuccesfull = false, ExceptionMessage = exc.Message, Response = new CheckMedicineAvailabilityResponseDTO() };
            }
        }

        public static MedicineProcurementGrpcResponseDTO UrgentMedicineProcurement(CreateMedicineRequestForPharmacyDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            try
            {
                var channel = new Channel(pharmacy.BaseUrl, ChannelCredentials.Insecure);
                var client = new MedicineInventoryService.MedicineInventoryServiceClient(channel);
                var input = new MedicineProcurementRequestModel { ApiKey = pharmacy.ApiKey.ToString(), ManufacturerName = medicineRequestDTO.ManufacturerName, MedicineName = medicineRequestDTO.MedicineName, Quantity = medicineRequestDTO.Quantity };
                var response = client.EmergencyMedicineProcurement(input);
                return new MedicineProcurementGrpcResponseDTO { ConnectionSuccesfull = true, Response = new MedicineProcurementResponseDTO { Answer = response.Answer, ExceptionMessage = response.ExceptionMessage } };
            }
            catch (Exception exc)
            {
                return new MedicineProcurementGrpcResponseDTO { ConnectionSuccesfull = false, Response = new MedicineProcurementResponseDTO { ExceptionMessage = exc.Message } };
            }
        }
    }
}
