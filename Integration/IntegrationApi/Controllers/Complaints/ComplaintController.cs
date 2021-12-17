using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using IntegrationAPI.DTO.Complaints;

namespace IntegrationAPI.Controllers.Complaints
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private ComplaintMasterService _complaintMasterService;
        private PharmacyMasterService _pharmacyMasterService;

        public ComplaintController(IUnitOfWork unitOfWork)
        {
            _complaintMasterService = new ComplaintMasterService(unitOfWork);
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpGet]
        public IEnumerable<Complaint> GetComplaints()
        {
            IEnumerable<Complaint> complaints = _complaintMasterService.GetComplaints();
            foreach (Complaint complaint in complaints)
            {
                complaint.Pharmacy.Complaints = null;
                if (complaint.ComplaintResponse != null) complaint.ComplaintResponse.Complaint = null;
            }
            return complaints;
        }
        [HttpGet("{id:int}")]
        public Complaint GetComplaintById(int id)
        {
            Complaint complaint = _complaintMasterService.GetComplaintById(id);
            complaint.Pharmacy.Complaints = null;
            if (complaint.ComplaintResponse != null) complaint.ComplaintResponse.Complaint = null;
            return complaint;
        }
        [HttpPost]
        public IActionResult SendComplaint(CreateComplaintDTO createComplaintDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createComplaintDTO.PharmacyId);
            if (pharmacy == null) return BadRequest("Pharmacy does not exist");
            Complaint complaint = new Complaint
            {
                Title = createComplaintDTO.Title,
                Description = createComplaintDTO.Description,
                PharmacyId = pharmacy.Id,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                ManagerId = 1
            };
            _complaintMasterService.SaveComplaint(complaint);
            ComplaintDTO complaintDTO = new ComplaintDTO
            {
                ApiKey = complaint.Pharmacy.ApiKey,
                CreatedDateTime = complaint.CreatedDate.ToUniversalTime(),
                Description = complaint.Description,
                Title = complaint.Title,
            };
            IRestResponse response = SendComplaintToPharmacy(pharmacy.BaseUrl, complaintDTO);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _complaintMasterService.DeleteComplaint(complaint);
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }
            return Ok("Complaint saved and sent to pharmacy!");
        }
        private IRestResponse SendComplaintToPharmacy(string baseUrl, ComplaintDTO complaintDTO)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(baseUrl + "/api/Complaint/CreateComplaint");
            request.AddJsonBody(complaintDTO);
            IRestResponse response = client.Post(request);
            return response;
        }
    }
}
