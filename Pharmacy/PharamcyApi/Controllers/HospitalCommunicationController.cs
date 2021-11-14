using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharamcyApi.Adapters;
using PharamcyApi.DTO;
using Pharmacy.MasterServices;
using RestSharp;

namespace PharamcyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalCommunicationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private HospitalService hospitalService;
        private ComplaintService complaintService;
        private ComplaintResponseService complaintResponseService;
        public HospitalCommunicationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            hospitalService = new HospitalService(unitOfWork);
            complaintService = new ComplaintService(unitOfWork);
            complaintResponseService = new ComplaintResponseService(unitOfWork);
        }

        public IActionResult AcceptHospitalRegistration(Hospital newHospital)
        {
            if (!hospitalService.isUnique(newHospital))
            {
                return BadRequest("Hospital already exists!");
            }
            hospitalService.SaveHospital(newHospital);
            return Ok();
        }
        [HttpGet]
        public IActionResult PingResponse(string apiKey)
        {
            Hospital hospital = hospitalService.FindHospitalByApiKey(apiKey);
            if (hospital == null)
            {
                return Ok("Pharmacy is not registered");
            }
            return Ok("Hospital responds to ping from " + hospital.Name);
        }
        [HttpPost]
        public IActionResult PostComplaint(ComplaintDTO complaintDTO)
        {
            Hospital hospital = hospitalService.FindHospitalByApiKey(complaintDTO.ApiKey);
            if (hospital == null) return BadRequest("Hospital not registered!");
            Complaint complaint = ComplaintAdapter.ComplaintDTOToComplaint(complaintDTO, hospital);
            complaintService.SaveComplaint(complaint);
            return Ok("Complaint received!");
        }
        [HttpPost]
        public IActionResult PostComplaintResponse(ComplaintResponse complaintResponse)
        {
            complaintResponse.Complaint = complaintService.GetComplaintById(complaintResponse.ComplaintId);
            complaintResponse.CreatedDate = DateTime.Now;
            IRestResponse response = SendComplaintResponse(complaintResponse);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Response failed to send");
            }
            complaintResponseService.SaveComplaintResponse(complaintResponse);
            return Ok();
        }

        private IRestResponse SendComplaintResponse(ComplaintResponse complaintResponse)
        {
            ComplaintResponseDTO responseDTO =
                ComplaintResponseAdapter.ComplaintResponseToComplaintResponseDTO(complaintResponse);
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(complaintResponse.Complaint.Hospital.BaseUrl +
                                                  "api/PharmacyCommunication/PostComplaintResponse");
            request.AddJsonBody(responseDTO);
            IRestResponse response = client.Post(request);
            return response;
        }
    }
}
