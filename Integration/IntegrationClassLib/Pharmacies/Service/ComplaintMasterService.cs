using System.Collections.Generic;
using System.Linq;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Pharmacies.Service
{
    public class ComplaintMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public ComplaintMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Complaint> GetComplaints()
        {
            var complaintsRepo = unitOfWork.GetRepository<IComplaintReadRepository>();
            IEnumerable<Complaint> complaints = complaintsRepo.GetAll().Include(x => x.ComplaintResponse).Include(x => x.Manager).Include(x => x.Pharmacy);
            return complaints;
        }
        public Complaint GetComplaintById(int id)
        {
            IEnumerable<Complaint> complaints = GetComplaints();
            Complaint complaint = complaints.FirstOrDefault(complaint => complaint.Id == id);
            return complaint;
        }
        public void SaveComplaint(Complaint complaint)
        {
            var complaintRepo = unitOfWork.GetRepository<IComplaintWriteRepository>();
            complaintRepo.Add(complaint);
        }
        public void DeleteComplaint(Complaint complaint)
        {
            var complaintRepo = unitOfWork.GetRepository<IComplaintWriteRepository>();
            complaintRepo.Delete(complaint);
        }
    }
}
