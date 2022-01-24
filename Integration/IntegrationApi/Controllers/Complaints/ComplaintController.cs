using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using IntegrationAPI.DTO.Complaints;
using IntegrationApi.Messages;

namespace IntegrationAPI.Controllers.Complaints
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
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
        public IActionResult GetComplaintById(int id)
        {
            Complaint complaint = _complaintMasterService.GetComplaintById(id);
            if (complaint == null) return NotFound(ComplaintMessages.WrongId);
            complaint.Pharmacy.Complaints = null;
            if (complaint.ComplaintResponse != null) complaint.ComplaintResponse.Complaint = null;
            return Ok(complaint);
        }
        [HttpPost]
        public IActionResult SendComplaint(CreateComplaintDTO createComplaintDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createComplaintDTO.PharmacyId);
            if (pharmacy == null) return BadRequest(PharmacyMessages.WrongId);

            var timeZoneDif = DateTime.Now - DateTime.Now.ToUniversalTime();
            var complaint = CreateComplaint(createComplaintDTO, pharmacy, timeZoneDif);
            _complaintMasterService.SaveComplaint(complaint);

            var complaintDTO = CreateComplaintDto(complaint);
            IRestResponse response = SendComplaintToPharmacy(pharmacy.BaseUrl, complaintDTO);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _complaintMasterService.DeleteComplaint(complaint);
                return Problem(ComplaintMessages.DidNotReceive);
            }
            return Ok(ComplaintMessages.Received);
        }

        private static ComplaintDTO CreateComplaintDto(Complaint complaint)
        {
            ComplaintDTO complaintDTO = new ComplaintDTO
            {
                ApiKey = complaint.Pharmacy.ApiKey,
                CreatedDateTime = complaint.CreatedDate,
                Description = complaint.Description,
                Title = complaint.Title,
            };
            return complaintDTO;
        }

        private static Complaint CreateComplaint(CreateComplaintDTO createComplaintDTO, Pharmacy pharmacy, TimeSpan timeZoneDif)
        {
            Complaint complaint = new Complaint
            {
                Title = createComplaintDTO.Title,
                Description = createComplaintDTO.Description,
                PharmacyId = pharmacy.Id,
                CreatedDate = DateTime.Now.ToUniversalTime() + timeZoneDif,
                ManagerId = 1
            };
            return complaint;
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
