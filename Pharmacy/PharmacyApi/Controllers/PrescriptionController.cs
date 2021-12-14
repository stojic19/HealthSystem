using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.Controllers.Base;
using PharmacyApi.DTO;
using Renci.SshNet;

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
        public IActionResult ReceivePrescriptionHttp(PrescriptionHttpDto prescriptionHttpDto)
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

        [HttpPost]
        public IActionResult ReceivePrescriptionSftp(PrescriptionSftpDto prescriptionSftpDto)
        {
            if (!IsApiKeyValid(prescriptionSftpDto.ApiKey))
            {
                return BadRequest(ModelState);
            }

            try
            {
                SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(_sftpCredentials.Host, _sftpCredentials.Username, _sftpCredentials.Password));
                sftpClient.Connect();
                Stream fileStream = System.IO.File.OpenWrite("Prescriptions" + Path.DirectorySeparatorChar + prescriptionSftpDto.FileName);
                sftpClient.DownloadFile(prescriptionSftpDto.FileName, fileStream);
                sftpClient.Disconnect();
                fileStream.Close();

                return Ok("Done");
            }
            catch
            {
                return BadRequest("Failed to save prescription");
            }

        }
    }
}
