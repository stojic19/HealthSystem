using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : BasePharmacyController
    {
        public ReportController(IUnitOfWork uow, PharmacyDetails details) : base(uow, details)
        {
        }
        [HttpPost]
        public IActionResult ReceiveMedicineConsumptionReport(ConsumptionReportDTO consumptionReportDto)
        {
            if (!IsApiKeyValid(consumptionReportDto.ApiKey))
            {
                return BadRequest(ModelState);
            }
            Hospital hospital = UoW.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .Where(x => x.ApiKey == consumptionReportDto.ApiKey).First();
            MedicineReportFile medicineReportFile = new MedicineReportFile
            {
                HospitalId = hospital.Id,
                FileName = consumptionReportDto.FileName,
                Host = consumptionReportDto.Host
            };
            try
            {
                UoW.GetRepository<IMedicineReportFileWriteRepository>().Add(medicineReportFile);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
            return Ok();
        }
    }
}
