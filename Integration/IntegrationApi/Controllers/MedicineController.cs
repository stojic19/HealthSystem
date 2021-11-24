using Integration.MasterServices;
using Integration.Model;
using Integration.Repositories.Base;
using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            CheckMedicineAvailabilityRequestDTO medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToMedicineInformationRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendMedicineRequestToPharmacy(medicineRequestDTO, pharmacy);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
            CheckMedicineAvailabilityResponseDTO responseDTO = JsonConvert.DeserializeObject <CheckMedicineAvailabilityResponseDTO>(response.Content);
            return Ok(responseDTO);
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
            MedicineProcurementRequestDTO medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToEmergencyProcurementRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendUrgentProcurementRequestToPharmacy(medicineRequestDTO, pharmacy);
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.NotFound && response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return BadRequest("Pharmacy failed to receive request! Try again");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _medicineInventoryMasterService.AddMedicineToInventory(medicineRequestDTO.MedicineName, medicineRequestDTO.Quantity);
            }  
            return Ok(response.Content);
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
