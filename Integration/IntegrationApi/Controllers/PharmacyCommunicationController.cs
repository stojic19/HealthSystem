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

namespace Integration.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PharmacyCommunicationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
 //       private PharmacyService pharmacyService;
 //       private ComplaintService complaintService;
 //       private ComplaintResponseService complaintResponseService;

        public PharmacyCommunicationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
//            pharmacyService = new PharmacyService(unitOfWork);
//            complaintService = new ComplaintService(unitOfWork);
//            complaintResponseService = new ComplaintResponseService(unitOfWork);
        }
        [HttpPost]
        public IActionResult RegisterPharmacy(PharmacyDTO pharmacyDTO)
        {
            Pharmacy existingPharmacy = null;//pharmacyService.FindPharmacyByName(pharmacyDTO.Name);
            if (existingPharmacy != null)
            {
                return BadRequest("Pharmacy already exists!");
            }

            Pharmacy pharmacy = PharmacyAdapter.PharmacyDTOToPharmacy(pharmacyDTO);
            pharmacy.ApiKey = Guid.NewGuid();
            HospitalDTO dto = CreatePostData(pharmacy, Program.hospitalUrl);

            IRestResponse response = SendRegistrationPost(pharmacyDTO, dto);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }

//            pharmacyService.SavePharmacy(pharmacy);
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
            Pharmacy existingPharmacy = null;//pharmacyService.FindPharmacyByName(pharmacyName);

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
            Pharmacy existingPharmacy = null;//pharmacyService.FindPharmacyByApiKey(apiKey);

            if (existingPharmacy == null)
            {
                return Ok("Pharmacy is not registered");
            }

            return Ok("Hospital responds to ping from " + existingPharmacy.Name);
        }
        [HttpPost]
        public IActionResult PostComplaint(CreateComplaintDTO createComplaintDTO)
        {
            Complaint complaint = null;//ComplaintAdapter.CreateComplaintDTOToComplaint(createComplaintDTO, pharmacyService.GetPharmacyById(createComplaintDTO.PharmacyId));
            //complaintService.SaveComplaint(complaint);
            ComplaintDTO complaintDTO = ComplaintAdapter.ComplaintToComplaintDTO(complaint);                       
            IRestResponse response = SendComplaintToHospital(complaint, complaintDTO);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
//                complaintService.DeleteComplaint(complaint);
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
            Pharmacy pharmacy = null;//pharmacyService.FindPharmacyByApiKey(complaintResponseDTO.ApiKey);
            if (pharmacy == null) return BadRequest("Pharmacy not registered");
            //complaintResponseService.SaveComplaintResponse(complaintResponseDTO);

            return Ok("Complaint response received!");
        }
    }
}
