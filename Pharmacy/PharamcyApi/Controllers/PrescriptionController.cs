using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : BasePharmacyController
    {
        public PrescriptionController(IUnitOfWork uow, PharmacyDetails pharmacyDetails) : base(uow, pharmacyDetails)
        {
        }

        [HttpPost]
        public IActionResult ReceivePrescription(PrescriptionHttpDto prescriptionHttpDto)
        {
            if (!IsApiKeyValid(prescriptionHttpDto.ApiKey))
            {
                return BadRequest(ModelState);
            }

            try
            {
                System.IO.File.WriteAllBytes(
                    "Prescriptions" + Path.DirectorySeparatorChar + prescriptionHttpDto.FileName,
                    prescriptionHttpDto.FileContent);
                return Ok("Done");
            }
            catch
            {
                return BadRequest("Failed to save prescription");
            }
            
        }
    }
}
