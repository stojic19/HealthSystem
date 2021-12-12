using Grpc.Core;
using Grpc.Net.Client;
using Integration.Pharmacies.Model;
using IntegrationAPI.DTO;
using IntegrationAPI.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.GrpcServices
{
    public static class MedicineInventoryGrpcService
    {
        public static CheckMedicineAvailabilityGrpcResponseDto CheckMedicineAvailability(CreateMedicineRequestForPharmacyDto medicineRequestDTO, Pharmacy pharmacy)
        {
            try
            {
                var channel = new Channel(pharmacy.BaseUrl, ChannelCredentials.Insecure);
                var client = new MedicineInventoryService.MedicineInventoryServiceClient(channel);
                var input = new CheckMedicineAvailabilityRequestModel() { ApiKey = pharmacy.ApiKey.ToString(), ManufacturerName = medicineRequestDTO.ManufacturerName, MedicineName = medicineRequestDTO.MedicineName, Quantity = medicineRequestDTO.Quantity };
                var response = client.CheckMedicineAvailability(input);
                return new CheckMedicineAvailabilityGrpcResponseDto { ConnectionSuccesfull = true, ExceptionMessage = response.ExceptionMessage, Response = new CheckMedicineAvailabilityResponseDto { Answer = response.Answer } };
            }
            catch (Exception exc)
            {
                return new CheckMedicineAvailabilityGrpcResponseDto { ConnectionSuccesfull = false, ExceptionMessage = exc.Message, Response = new CheckMedicineAvailabilityResponseDto() };
            }
        }

        public static MedicineProcurementGrpcResponseDto UrgentMedicineProcurement(CreateMedicineRequestForPharmacyDto medicineRequestDTO, Pharmacy pharmacy)
        {
            try
            {
                var channel = new Channel(pharmacy.BaseUrl, ChannelCredentials.Insecure);
                var client = new MedicineInventoryService.MedicineInventoryServiceClient(channel);
                var input = new MedicineProcurementRequestModel { ApiKey = pharmacy.ApiKey.ToString(), ManufacturerName = medicineRequestDTO.ManufacturerName, MedicineName = medicineRequestDTO.MedicineName, Quantity = medicineRequestDTO.Quantity };
                var response = client.EmergencyMedicineProcurement(input);
                return new MedicineProcurementGrpcResponseDto { ConnectionSuccesfull = true, Response = new MedicineProcurementResponseDto { Answer = response.Answer, ExceptionMessage = response.ExceptionMessage } };
            }
            catch (Exception exc)
            {
                return new MedicineProcurementGrpcResponseDto { ConnectionSuccesfull = false, Response = new MedicineProcurementResponseDto { ExceptionMessage = exc.Message } };
            }
        }
    }
}
