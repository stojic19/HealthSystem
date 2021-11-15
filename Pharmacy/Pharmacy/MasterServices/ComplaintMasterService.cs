using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;

namespace Pharmacy.MasterServices
{
    public class ComplaintMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public ComplaintMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void SaveComplaint(Complaint complaint)
        {
            var complaintRepo = unitOfWork.GetRepository<IComplaintWriteRepository>();
            complaintRepo.Add(complaint);
        }

        public Complaint GetComplaintById(int id)
        {
            var complaintRepo = unitOfWork.GetRepository<IComplaintReadRepository>();
            IEnumerable<Complaint> complaints = complaintRepo.GetAll().Include(x => x.Hospital);
            foreach (Complaint complaint in complaints)
                if (complaint.Id == id)
                    return complaint;
            return null;
        }
    }
}
