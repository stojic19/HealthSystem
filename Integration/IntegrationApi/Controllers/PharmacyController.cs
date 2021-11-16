using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using Integration.MasterServices;
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
        private PharmacyMasterService _pharmacyMasterService;

        public PharmacyController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpGet]
        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return _pharmacyMasterService.GetPharmacies();
        }
    }
}
