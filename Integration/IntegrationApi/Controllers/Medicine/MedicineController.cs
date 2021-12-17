using Integration.Partnership.Service;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Repository.Base;
using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using IntegrationAPI.DTO.MedicineProcurement;
using IntegrationAPI.GrpcServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace IntegrationAPI.Controllers.Medicine
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private PharmacyMasterService _pharmacyMasterService;

        public MedicineController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpPost]
        public IActionResult RequestMedicineInformation(CreateMedicineRequestForPharmacyDto createMedicineRequestDTO)
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
            if(pharmacy.GrpcSupported)
            {
                return CheckMedicineAvailabilityGrpc(createMedicineRequestDTO, pharmacy);
            }
            CheckMedicineAvailabilityRequestDto medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToMedicineInformationRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendMedicineRequestToPharmacy(medicineRequestDTO, pharmacy);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
            CheckMedicineAvailabilityResponseDto responseDTO = JsonConvert.DeserializeObject <CheckMedicineAvailabilityResponseDto>(response.Content);
            return Ok(responseDTO);
        }

        private IActionResult CheckMedicineAvailabilityGrpc(CreateMedicineRequestForPharmacyDto createMedicineRequestDTO, Pharmacy pharmacy)
        {
            CheckMedicineAvailabilityGrpcResponseDto grpcResponseDTO = MedicineInventoryGrpcService.CheckMedicineAvailability(createMedicineRequestDTO, pharmacy);
            if(grpcResponseDTO.ConnectionSuccesfull)
            {
                return Ok(grpcResponseDTO.Response);
            }
            else
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
        }

        private static IRestResponse SendMedicineRequestToPharmacy(CheckMedicineAvailabilityRequestDto medicineRequestDTO, Pharmacy pharmacy)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/MedicineProcurement/check";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }

        [HttpPost]
        public IActionResult UrgentProcurementOfMedicine(CreateMedicineRequestForPharmacyDto createMedicineRequestDTO)
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
            if (pharmacy.GrpcSupported)
            {
                return MedicineProcurementGrpc(createMedicineRequestDTO, pharmacy);
            }
            MedicineProcurementRequestDto medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToEmergencyProcurementRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendUrgentProcurementRequestToPharmacy(medicineRequestDTO, pharmacy);
            MedicineProcurementResponseDto responseDTO = new MedicineProcurementResponseDto();
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseDTO.Answer = true;
                response = SendMedicineToHospital(new AddMedicineRequestDto() { MedicineName = createMedicineRequestDTO.MedicineName, Quantity = createMedicineRequestDTO.Quantity });
                MedicineProcurementHospitalResponseDto responseFromHospitalDTO = JsonConvert.DeserializeObject<MedicineProcurementHospitalResponseDto>(response.Content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseDTO.ExceptionMessage = responseFromHospitalDTO.Answer;
                    return Ok(responseDTO);
                }
                responseDTO.ExceptionMessage = response.Content;
                return BadRequest(responseDTO);
            }
            return Ok(responseDTO);
        }

        private IActionResult MedicineProcurementGrpc(CreateMedicineRequestForPharmacyDto createMedicineRequestDTO, Pharmacy pharmacy)
        {
            MedicineProcurementGrpcResponseDto grpcResponseDTO = MedicineInventoryGrpcService.UrgentMedicineProcurement(createMedicineRequestDTO, pharmacy);
            if (grpcResponseDTO.ConnectionSuccesfull)
            {
                if (grpcResponseDTO.Response.Answer) 
                {
                    IRestResponse response = SendMedicineToHospital(new AddMedicineRequestDto() { MedicineName = createMedicineRequestDTO.MedicineName, Quantity = createMedicineRequestDTO.Quantity });
                    MedicineProcurementHospitalResponseDto responseFromHospitalDTO = JsonConvert.DeserializeObject<MedicineProcurementHospitalResponseDto>(response.Content);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        grpcResponseDTO.Response.ExceptionMessage = responseFromHospitalDTO.Answer;
                        return Ok(grpcResponseDTO);
                    }
                    grpcResponseDTO.Response.ExceptionMessage = response.Content;
                    return BadRequest(grpcResponseDTO);
                }
                return Ok(grpcResponseDTO.Response);
            }
            else
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
        }

        private static IRestResponse SendUrgentProcurementRequestToPharmacy(MedicineProcurementRequestDto medicineRequestDTO, Pharmacy pharmacy)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/MedicineProcurement/execute";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }

        private static IRestResponse SendMedicineToHospital(AddMedicineRequestDto medicineRequestDTO)
        {
            RestClient client = new RestClient();
            string targetUrl = "https://localhost:44303/api/Medication/AddMedicineQuantity";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }
    }
}
