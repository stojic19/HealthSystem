using Integration.Model;
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

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private ComplaintService complaintService;

        public ComplaintController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            complaintService = new ComplaintService(unitOfWork);
        }

        [HttpGet]
        public IEnumerable<Complaint> GetComplaints()
        {
            IEnumerable<Complaint> complaints = complaintService.GetComplaints();
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
            Complaint complaint = complaintService.GetComplaintById(id);
            complaint.Pharmacy.Complaints = null;
            if (complaint.ComplaintResponse != null) complaint.ComplaintResponse.Complaint = null;
            return complaint;
        }
    }
}
