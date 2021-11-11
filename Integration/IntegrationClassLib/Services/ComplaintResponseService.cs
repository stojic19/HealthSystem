using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Services
{
    public class ComplaintResponseService
    {
        private readonly IUnitOfWork unitOfWork;
        public ComplaintResponseService(IUnitOfWork unitOfWork)
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
