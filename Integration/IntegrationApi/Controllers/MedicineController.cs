using Integration.Partnership.Service;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Repository.Base;
using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using IntegrationAPI.gRPCServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private PharmacyMasterService _pharmacyMasterService;
        private MedicineInventoryMasterService _medicineInventoryMasterService;

        public MedicineController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
            _medicineInventoryMasterService = new MedicineInventoryMasterService(unitOfWork);
        }

        [HttpPost]
        public IActionResult RequestMedicineInformation(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO)
        {
            if (createMedicineRequestDTO.Quantity <= 0)
            {
                return BadRequest("Invalid quantity.");
            }
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createMedicineRequestDTO.PharmacyId);
            if (pharmacy==null)
            {
                return BadRequest("Pharmacy id doesn't exist.");
            }
            if(createMedicineRequestDTO.GrpcCommunication)
            {
                return CheckMedicineAvailabilityGrpc(createMedicineRequestDTO, pharmacy);
            }
            CheckMedicineAvailabilityRequestDTO medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToMedicineInformationRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendMedicineRequestToPharmacy(medicineRequestDTO, pharmacy);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
            CheckMedicineAvailabilityResponseDTO responseDTO = JsonConvert.DeserializeObject <CheckMedicineAvailabilityResponseDTO>(response.Content);
            return Ok(responseDTO);
        }

        private IActionResult CheckMedicineAvailabilityGrpc(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            CheckMedicineAvailabilityGrpcResponseDTO grpcResponseDTO = MedicineInventorygRPCService.CheckMedicineAvailability(createMedicineRequestDTO, pharmacy);
            if(grpcResponseDTO.ConnectionSuccesfull)
            {
                return Ok(grpcResponseDTO.Response);
            }
            else
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
        }

        private IRestResponse SendMedicineRequestToPharmacy(CheckMedicineAvailabilityRequestDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/MedicineProcurement/check";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }

        [HttpPost]
        public IActionResult UrgentProcurementOfMedicine(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO)
        {
            if (createMedicineRequestDTO.Quantity <= 0)
            {
                return BadRequest("Invalid quantity.");
            }
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createMedicineRequestDTO.PharmacyId);
            if (pharmacy == null)
            {
                return BadRequest("Pharmacy id doesn't exist.");
            }
            if (createMedicineRequestDTO.GrpcCommunication)
            {
                return MedicineProcurementGrpc(createMedicineRequestDTO, pharmacy);
            }
            MedicineProcurementRequestDTO medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToEmergencyProcurementRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendUrgentProcurementRequestToPharmacy(medicineRequestDTO, pharmacy);
            MedicineProcurementResponseDTO responseDTO = new MedicineProcurementResponseDTO();
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseDTO.Answer = true;
                responseDTO.NotificationText = response.Content;
                _medicineInventoryMasterService.AddMedicineToInventory(medicineRequestDTO.MedicineName, medicineRequestDTO.Quantity);
            }  
            return Ok(responseDTO);
        }

        private IActionResult MedicineProcurementGrpc(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO, Pharmacy pharmacy)
        {
            MedicineProcurementGrpcResponseDTO grpcResponseDTO = MedicineInventorygRPCService.UrgentMedicineProcurement(createMedicineRequestDTO, pharmacy);
            if (grpcResponseDTO.ConnectionSuccesfull)
            {
                return Ok(grpcResponseDTO.Response);
            }
            else
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
        }

        private IRestResponse SendUrgentProcurementRequestToPharmacy(MedicineProcurementRequestDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/MedicineProcurement/execute";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }
    }
}
