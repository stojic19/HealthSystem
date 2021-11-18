using Integration.DTO;
using Integration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Integration.Repositories.Base;
using Integration.Repositories;
using Microsoft.EntityFrameworkCore;
using IntegrationAPI.DTO;
using IntegrationAPI.Adapters;
using Integration.MasterServices;

namespace Integration.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PharmacyCommunicationController : ControllerBase
    {
        private PharmacyMasterService _pharmacyMasterService;
        private ComplaintMasterService _complaintMasterService;
        private ComplaintResponseMasterService _complaintResponseMasterService;

        public PharmacyCommunicationController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
            _complaintMasterService = new ComplaintMasterService(unitOfWork);
            _complaintResponseMasterService = new ComplaintResponseMasterService(unitOfWork);
        }
        [HttpPost, Produces("application/json")]
        public IActionResult RegisterPharmacy(PharmacyDTO pharmacyDTO)
        {
            Pharmacy pharmacy = PharmacyAdapter.PharmacyDTOToPharmacy(pharmacyDTO);
            if (!_pharmacyMasterService.isUnique(pharmacy))
            {
                return BadRequest("Pharmacy already exists!");
            }
            pharmacy.ApiKey = Guid.NewGuid();
            HospitalDTO dto = CreatePostData(pharmacy, Program.hospitalUrl);

            IRestResponse response = SendRegistrationPost(pharmacyDTO, dto);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Failed to contact pharmacy!");
            }
            _pharmacyMasterService.SavePharmacy(pharmacy);
            return Ok("Pharmacy registered");
        }
        private static HospitalDTO CreatePostData(Pharmacy pharmacy, string hospitalUrl)
        {
            Country country = new Country { Name = "Srbija" };
            City city = new City { Name = "Novi Sad", PostalCode = 21000, Country = country };
            HospitalDTO dto = new HospitalDTO { ApiKey = pharmacy.ApiKey, BaseUrl = hospitalUrl, Name = "Nasa bolnica", StreetName = "Vojvode Stepe", StreetNumber = "14", City = city };
            return dto;
        }
        private IRestResponse SendRegistrationPost(PharmacyDTO pharmacyDTO, HospitalDTO dto)
        {
            RestClient client = new RestClient();
            string targetUrl = pharmacyDTO.BaseUrl + "/api/HospitalCommunication/AcceptHospitalRegistration";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(dto);
            return client.Post(request);
        }

        [HttpGet]
        public IActionResult PingPharmacy(string pharmacyName)
        {
            Pharmacy existingPharmacy = _pharmacyMasterService.FindPharmacyByName(pharmacyName);

            if (existingPharmacy == null) return BadRequest("Pharmacy with that name does not exist in database");
            IRestResponse response = SendPingToHospital(existingPharmacy);
            return Ok(response.Content);
        }
        private static IRestResponse SendPingToHospital(Pharmacy existingPharmacy)
        {
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(existingPharmacy.BaseUrl + "/api/hospitalCommunication/PingResponse/?apiKey=" + existingPharmacy.ApiKey);
            IRestResponse response = client.Get(request);
            return response;
        }
        [HttpGet]
        public IActionResult PingResponse(string apiKey)
        {
            Pharmacy existingPharmacy = _pharmacyMasterService.FindPharmacyByApiKey(apiKey);

            if (existingPharmacy == null)
            {
                return Ok("Pharmacy is not registered");
            }

            return Ok("Hospital responds to ping from " + existingPharmacy.Name);
        }
        [HttpPost, Produces("application/json")]
        public IActionResult PostComplaint(CreateComplaintDTO createComplaintDTO)
        {
            Complaint complaint = ComplaintAdapter.CreateComplaintDTOToComplaint(createComplaintDTO, _pharmacyMasterService.GetPharmacyById(createComplaintDTO.PharmacyId));
            _complaintMasterService.SaveComplaint(complaint);
            ComplaintDTO complaintDTO = ComplaintAdapter.ComplaintToComplaintDTO(complaint);                       
            IRestResponse response = SendComplaintToHospital(complaint, complaintDTO);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                _complaintMasterService.DeleteComplaint(complaint);
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }
            return Ok("Complaint saved and sent to pharmacy!");
        }
        private static IRestResponse SendComplaintToHospital(Complaint complaint, ComplaintDTO complaintDTO)
        {
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(complaint.Pharmacy.BaseUrl + "/api/hospitalCommunication/PostComplaint");
            request.AddJsonBody(complaintDTO);
            IRestResponse response = client.Post(request);
            return response;
        }
        [HttpPost]
        public IActionResult PostComplaintResponse(ComplaintResponseDTO complaintResponseDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.FindPharmacyByApiKey(complaintResponseDTO.ApiKey);
            if (pharmacy == null) return BadRequest("Pharmacy not registered");
            ComplaintResponse complaintResponse = ComplaintResponseAdapter.ComplaintResponseDTOToComplaintResponse(complaintResponseDTO);
            _complaintResponseMasterService.SaveComplaintResponse(complaintResponse);

            return Ok("Complaint response received!");
        }
    }
}
