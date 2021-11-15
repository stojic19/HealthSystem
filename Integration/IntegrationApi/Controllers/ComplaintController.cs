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
        private ComplaintMasterService _complaintMasterService;

        public ComplaintController(IUnitOfWork unitOfWork)
        {
            _complaintMasterService = new ComplaintMasterService(unitOfWork);
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
    }
}
