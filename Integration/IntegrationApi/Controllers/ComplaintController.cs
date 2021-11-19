﻿using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using Integration.MasterServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration;
using Integration.DTO;
using IntegrationAPI.Adapters;
using RestSharp;

namespace IntegrationAPI.Controllers
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
        public IActionResult PostComplaint(CreateComplaintDTO createComplaintDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(createComplaintDTO.PharmacyId);
            Complaint complaint = new Complaint
            {
                Title = createComplaintDTO.Title,
                Description = createComplaintDTO.Description,
                PharmacyId = pharmacy.Id,
                CreatedDate = DateTime.Now,
                ManagerId = Program.ManagerId
            };
            _complaintMasterService.SaveComplaint(complaint);
            ComplaintDTO complaintDTO = new ComplaintDTO
            {
                ApiKey = complaint.Pharmacy.ApiKey,
                CreatedDate = complaint.CreatedDate,
                Description = complaint.Description,
                Title = complaint.Title,
            };
            IRestResponse response = SendComplaintToPharmacy(complaint, complaintDTO);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _complaintMasterService.DeleteComplaint(complaint);
                return BadRequest("Pharmacy failed to receive complaint! Try again");
            }
            return Ok("Complaint saved and sent to pharmacy!");
        }
        private IRestResponse SendComplaintToPharmacy(Complaint complaint, ComplaintDTO complaintDTO)
        {
            RestClient client = new RestSharp.RestClient();
            RestRequest request = new RestRequest(complaint.Pharmacy.BaseUrl + "/api/Complaint/CreateComplaint");
            request.AddJsonBody(complaintDTO);
            IRestResponse response = client.Post(request);
            return response;
        }
    }
}
