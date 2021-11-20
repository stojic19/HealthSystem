using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Repositories.Base;
using Integration.Model;
using Integration.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntegrationAPI.Controllers
{
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
            return benefit;
        }
        [HttpGet]
        public IEnumerable<Benefit> GetVisibleBenefits()
        {
            IEnumerable<Benefit> benefits = _uow.GetRepository<IBenefitReadRepository>().GetVisibleBenefits();
            return benefits;
        }

        [HttpPost]
        public IActionResult PublishBenefit(int id)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(id);
            if (benefit.Published)
            {
                return BadRequest("Benefit is already published");
            }
            benefit.Published = true;
            benefit = _uow.GetRepository<IBenefitWriteRepository>().Update(benefit);
            if (!benefit.Published)
            {
                return BadRequest("Error, could not publish benefit");
            }

            return Ok("Benefit published");
        }

        [HttpPost]
        public IActionResult HideBenefit(int id)
        {
            Benefit benefit = _uow.GetRepository<IBenefitReadRepository>().GetById(id);
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
