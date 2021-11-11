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
//        private PharmacyService pharmacyService;

        public PharmacyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
//            pharmacyService = new PharmacyService(unitOfWork);
        }

        [HttpGet]
        public IEnumerable<Pharmacy> GetPharmacies()
        {
            //            return pharmacyService.GetPharmacies();
            return null;
        }
    }
}
