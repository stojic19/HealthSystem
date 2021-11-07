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
    public class PharmacyController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PharmacyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Pharmacy> GetPharmacies()
        {
            var pharmacyRepo = unitOfWork.GetRepository<IPharmacyReadRepository>();
            IEnumerable<Pharmacy> pharmacies = pharmacyRepo.GetAll().Include(x => x.City).ThenInclude(x => x.Country);
            return pharmacies;
        }
    }
}
