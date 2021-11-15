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
        private HospitalMasterService _hospitalMasterService;
        private ComplaintMasterService _complaintMasterService;
        private ComplaintResponseMasterService _complaintResponseMasterService;
        public HospitalCommunicationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _hospitalMasterService = new HospitalMasterService(unitOfWork);
            _complaintMasterService = new ComplaintMasterService(unitOfWork);
            _complaintResponseMasterService = new ComplaintResponseMasterService(unitOfWork);
        }
        [HttpPost]
        public IActionResult AcceptHospitalRegistration(Hospital newHospital)
        {
            if (!_hospitalMasterService.isUnique(newHospital))
            {
                return BadRequest("Hospital already exists!");
            }
            _hospitalMasterService.SaveHospital(newHospital);
            return Ok();
        }
        [HttpGet]
        public IActionResult PingResponse(string apiKey)
        {
            Hospital hospital = _hospitalMasterService.FindHospitalByApiKey(apiKey);
            if (hospital == null)
            {
                return Ok("Pharmacy is not registered");
            }
            return Ok("Hospital responds to ping from " + hospital.Name);
        }
        [HttpPost]
        public IActionResult PostComplaint(ComplaintDTO complaintDTO)
        {
            Hospital hospital = _hospitalMasterService.FindHospitalByApiKey(complaintDTO.ApiKey);
            if (hospital == null) return BadRequest("Hospital not registered!");
            Complaint complaint = ComplaintAdapter.ComplaintDTOToComplaint(complaintDTO, hospital);
            _complaintMasterService.SaveComplaint(complaint);
            return Ok("Complaint received!");
        }
        [HttpPost]
        public IActionResult PostComplaintResponse(ComplaintResponse complaintResponse)
        {
            complaintResponse.Complaint = _complaintMasterService.GetComplaintById(complaintResponse.ComplaintId);
            complaintResponse.CreatedDate = DateTime.Now;
            IRestResponse response = SendComplaintResponse(complaintResponse);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Response failed to send");
            }
            _complaintResponseMasterService.SaveComplaintResponse(complaintResponse);
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
