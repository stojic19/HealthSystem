using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MasterServices
{
    public class BenefitMasterService
    {
        private readonly IUnitOfWork unitOfWork;
        public BenefitMasterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Benefit> GetBenefits()
        {
            var benefitsRepo = unitOfWork.GetRepository<IBenefitReadRepository>();
            IEnumerable<Benefit> benefits = benefitsRepo.GetAll().Include(x => x.Pharmacy);
            return benefits;
        }
        public Benefit GetBenefitById(int id)
        {
            IEnumerable<Benefit> benefits = GetBenefits();
            Benefit benefit = benefits.FirstOrDefault(benefit => benefit.Id == id);
            return benefit;
        }
        public void SaveBenefit(Benefit benefit)
        {
            var benefitsRepo = unitOfWork.GetRepository<IBenefitWriteRepository>();
            benefitsRepo.Add(benefit);
        }
        public void DeleteBenefit(Benefit benefit)
        {
            var benefitsRepo = unitOfWork.GetRepository<IBenefitWriteRepository>();
            benefitsRepo.Delete(benefit);
        }
    }
}
