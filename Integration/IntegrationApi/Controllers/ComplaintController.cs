using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
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

        public ComplaintController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Complaint> GetComplaints()
        {
            var repo = unitOfWork.GetRepository<IComplaintReadRepository>();
            IEnumerable<Complaint> retVal = repo.GetAll().Include(x => x.ComplaintResponse).Include(x => x.Manager).Include(x => x.Pharmacy);
            foreach(Complaint complaint in retVal)
            {
                complaint.Pharmacy.Complaints = null;
                if(complaint.ComplaintResponse != null) complaint.ComplaintResponse.Complaint = null;
            }
            return retVal;
        }
        [HttpGet]
        public Complaint GetComplaintById(int id)
        {
            var repo = unitOfWork.GetRepository<IComplaintReadRepository>();
            IEnumerable<Complaint> complaints = repo.GetAll().Include(x => x.ComplaintResponse).Include(x => x.Manager).Include(x => x.Pharmacy);
            Complaint retVal = complaints.FirstOrDefault(complaint => complaint.Id == id);
            retVal.Pharmacy.Complaints = null;
            if(retVal.ComplaintResponse != null)  retVal.ComplaintResponse.Complaint = null;
            return retVal;
        }
    }
}
