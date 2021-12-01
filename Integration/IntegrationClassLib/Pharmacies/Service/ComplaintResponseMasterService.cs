using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Service
{
    public class ComplaintResponseMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public ComplaintResponseMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void SaveComplaintResponse(ComplaintResponse complaintResponse)
        {
            var responseRepo = unitOfWork.GetRepository<IComplaintResponseWriteRepository>();
            responseRepo.Add(complaintResponse);
        }
    }
}
