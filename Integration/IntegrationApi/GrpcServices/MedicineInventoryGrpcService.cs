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
                var channel = new Channel("https://localhost:8787", ChannelCredentials.Insecure);
                var client = new MedicineInventory.MedicineInventoryClient(channel);
                var input = new CheckMedicineAvailabilityRequestModel() { ApiKey = pharmacy.ApiKey.ToString(), ManufacturerName = medicineRequestDTO.ManufacturerName, MedicineName = medicineRequestDTO.MedicineName, Quantity = medicineRequestDTO.Quantity };
                Console.WriteLine(pharmacy.BaseUrl);
                var response = client.CheckMedicineAvailabilityAsync(input);
                Console.WriteLine(response);
                return new CheckMedicineAvailabilityGrpcResponseDTO { ConnectionSuccesfull = true, Response = new CheckMedicineAvailabilityResponseDTO { Answer = response.ResponseAsync.Result.Answer } };
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.StackTrace);
                return new CheckMedicineAvailabilityGrpcResponseDTO { ConnectionSuccesfull = false, Response = new CheckMedicineAvailabilityResponseDTO() };
            }
        }

        public static MedicineProcurementGrpcResponseDTO UrgentMedicineProcurement(CreateMedicineRequestForPharmacyDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            try
            {
                var channel = GrpcChannel.ForAddress(pharmacy.BaseUrl);
                var medicineinventoryClient = new MedicineInventory.MedicineInventoryClient(channel);
                var informationForChecking = new MedicineProcurementRequestModel { ApiKey = pharmacy.ApiKey.ToString(), ManufacturerName = medicineRequestDTO.ManufacturerName, MedicineName = medicineRequestDTO.MedicineName, Quantity = medicineRequestDTO.Quantity };
                var response = medicineinventoryClient.EmergencyMedicineProcurement(informationForChecking);
                return new MedicineProcurementGrpcResponseDTO { ConnectionSuccesfull = true, Response = new MedicineProcurementResponseDTO { Answer = response.Answer } };
            }
            catch
            {
                return new MedicineProcurementGrpcResponseDTO { ConnectionSuccesfull = false, Response = new MedicineProcurementResponseDTO() };
            }
        }
    }
}
