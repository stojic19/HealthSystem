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

        public MedicineController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpPost]
        public IActionResult RequestMedicineInformation(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createMedicineRequestDTO.PharmacyId);
            if(pharmacy==null)
            {
                return BadRequest("Pharmacy id doesn't exist.");
            }
            MedicineRequestForPharmacyDTO medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToMedicineRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendMedicineRequestToPharmacy(medicineRequestDTO, pharmacy);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }
            MedicineInvetnoryResponseFromPharmacyDTO responseDTO = JsonConvert.DeserializeObject < MedicineInvetnoryResponseFromPharmacyDTO >(response.Content);
            return Ok(responseDTO);
        }

        private IRestResponse SendMedicineRequestToPharmacy(MedicineRequestForPharmacyDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/HospitalCommunication/AcceptHospitalRegistration";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }

        [HttpPost]
        public IActionResult UrgentProcurementOfMedicine(CreateMedicineRequestForPharmacyDTO createMedicineRequestDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createMedicineRequestDTO.PharmacyId);
            if (pharmacy == null)
            {
                return BadRequest("Pharmacy id doesn't exist.");
            }
            MedicineRequestForPharmacyDTO medicineRequestDTO = MedicineInventoryAdapter.CreateMedicineRequestToMedicineRequest(createMedicineRequestDTO, pharmacy);
            IRestResponse response = SendUrgentProcurementRequestToPharmacy(medicineRequestDTO, pharmacy);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }
            MedicineInvetnoryResponseFromPharmacyDTO responseDTO = JsonConvert.DeserializeObject<MedicineInvetnoryResponseFromPharmacyDTO>(response.Content);
            if(responseDTO.answer == true)
            {

                return Ok(responseDTO);
            }
            else
            {
                return Ok(responseDTO);
            }
        }

        private IRestResponse SendUrgentProcurementRequestToPharmacy(MedicineRequestForPharmacyDTO medicineRequestDTO, Pharmacy pharmacy)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/HospitalCommunication/AcceptHospitalRegistration";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(medicineRequestDTO);
            return client.Post(request);
        }
    }
}
