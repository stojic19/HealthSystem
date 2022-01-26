using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Pharmacies.Repository;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using IntegrationAPI.DTO.Benefits;
using IntegrationApi.Messages;

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
        [HttpGet("{id:int}"), Produces("application/json")]
        public IActionResult GetBenefitById(int id)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(id);
            if (benefit == null) return NotFound(BenefitMessages.WrongId);
            if (benefit.Pharmacy == null) benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
            return Ok(benefit);
        }
        [HttpGet]
        public IEnumerable<Benefit> GetVisibleBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetVisibleBenefits();
            foreach(var benefit in benefits)
                if(benefit.Pharmacy == null) benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
            return benefits;
        }

        [HttpGet]
        public IEnumerable<Benefit> GetPublishedBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetPublishedBenefits();
            foreach (var benefit in benefits)
                if (benefit.Pharmacy == null)
                    benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
            return benefits;
        }

        [HttpGet]
        public IEnumerable<Benefit> GetRelevantBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetRelevantBenefits();
            var pharmacies = _uow.GetRepository<IPharmacyReadRepository>().GetAll().ToList();
            foreach (var benefit in benefits)
                if (benefit.Pharmacy == null)
                    benefit.Pharmacy = _uow.GetRepository<IPharmacyReadRepository>().GetById(benefit.PharmacyId);
            return benefits;
        }

        [HttpPost, Produces("application/json")]
        public IActionResult PublishBenefit(BenefitIdDTO dto)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(dto.BenefitId);
            if (benefit.Published) return BadRequest(BenefitMessages.AlreadyPublished);
            benefit.Published = true;
            _uow.GetRepository<IBenefitWriteRepository>().Update(benefit);
            Benefit benefitNew = _uow.GetRepository<IBenefitReadRepository>().GetById(dto.BenefitId);
            if (!benefitNew.Published) return BadRequest(BenefitMessages.CannotPublish);
            return Ok(BenefitMessages.ConfirmPublish);
        }

        [HttpPost, Produces("application/json")]
        public IActionResult HideBenefit(BenefitIdDTO dto)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(dto.BenefitId);
            if (benefit.Hidden) return BadRequest(BenefitMessages.AlreadyHidden);
            benefit.Hidden = true;
            benefit = _uow.GetRepository<IBenefitWriteRepository>().Update(benefit);
            if (!benefit.Hidden) return BadRequest(BenefitMessages.CannotHide);
            return Ok(BenefitMessages.ConfirmHide);
        }
    }
}
