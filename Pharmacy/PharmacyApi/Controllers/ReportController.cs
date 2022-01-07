using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
using Renci.SshNet;

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
                Host = consumptionReportDto.Host,
                ReceivedDate = DateTime.Now
            };
            try
            {
                //TODO: promeniti kredencijale
                SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo("192.168.0.13", "tester", "password"));
                sftpClient.Connect();
                Stream fileStream = System.IO.File.OpenWrite("MedicineReports" + Path.DirectorySeparatorChar + consumptionReportDto.FileName);
                sftpClient.DownloadFile(consumptionReportDto.FileName, fileStream);
                sftpClient.Disconnect();
                fileStream.Close();
            }
            catch
            {
                return Problem("Failed to save file, error while trying to download from sftp");
            }
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
