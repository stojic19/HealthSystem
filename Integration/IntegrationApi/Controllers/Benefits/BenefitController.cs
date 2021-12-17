using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IntegrationAPI.DTO.Benefits;

namespace IntegrationAPI.Controllers.Benefits
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BenefitController : ControllerBase
    {
        private IUnitOfWork _uow;

        public BenefitController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IEnumerable<Benefit> GetBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetAll()
                .Include(x => x.Pharmacy);
            return benefits;
        }
        [HttpGet("{id:int}")]
        public Benefit GetBenefitById(int id)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(id);
            if (benefit.Pharmacy == null)
            {
                benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
            }
            return benefit;
        }
        [HttpGet]
        public IEnumerable<Benefit> GetVisibleBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetVisibleBenefits();
            foreach(var benefit in benefits)
            {
                if(benefit.Pharmacy == null)
                {
                    benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
                }
            }
            return benefits;
        }

        [HttpGet]
        public IEnumerable<Benefit> GetPublishedBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetPublishedBenefits();
            foreach (var benefit in benefits)
            {
                if (benefit.Pharmacy == null)
                {
                    benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
                }
            }
            return benefits;
        }

        [HttpGet]
        public IEnumerable<Benefit> GetRelevantBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetRelevantBenefits();
            foreach (var benefit in benefits)
            {
                if (benefit.Pharmacy == null)
                {
                    benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
                }
            }
            return benefits;
        }

        [HttpPost, Produces("application/json")]
        public IActionResult PublishBenefit(BenefitIdDTO dto)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(dto.BenefitId);
            if (benefit.Published)
            {
                return BadRequest("Benefit is already published");
            }
            benefit.Published = true;
            _uow.GetRepository<IBenefitWriteRepository>().Update(benefit);
            Benefit benefitNew = _uow.GetRepository<IBenefitReadRepository>().GetById(dto.BenefitId);
            if (!benefitNew.Published)
            {
                return BadRequest("Error, could not publish benefit");
            }

            return Ok("Benefit published");
        }

        [HttpPost, Produces("application/json")]
        public IActionResult HideBenefit(BenefitIdDTO dto)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(dto.BenefitId);
            if (benefit.Hidden)
            {
                return BadRequest("Benefit is already hidden");
            }
            benefit.Hidden = true;
            benefit = _uow.GetRepository<IBenefitWriteRepository>().Update(benefit);
            if (!benefit.Published)
            {
                return BadRequest("Error, could not hide benefit");
            }

            return Ok("Benefit hidden");
        }
    }
}
