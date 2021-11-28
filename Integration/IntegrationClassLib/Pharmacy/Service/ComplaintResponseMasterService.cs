using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacy.Service
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
