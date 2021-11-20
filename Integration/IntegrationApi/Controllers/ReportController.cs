using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Integration.MasterServices;
using Integration.Model;
using Integration.Repositories.Base;
using IntegrationAPI.DTO;
using RestSharp;
using Newtonsoft.Json;
using Renci.SshNet;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly PharmacyMasterService _pharmacyMasterService;
        private readonly MedicineConsumptionMasterService _medicineConsumptionMasterService;
        private readonly SftpMasterService _sftpMasterService;
        public ReportController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
            _sftpMasterService = new SftpMasterService();
            _medicineConsumptionMasterService = new MedicineConsumptionMasterService(unitOfWork);
        }

        [HttpPost]
        public MedicineConsumptionReport CreateConsumptionReport(TimeRange timeRange)
        {
            var report = _medicineConsumptionMasterService.CreateConsumptionReportInTimeRange(timeRange);
            report.MedicineConsumptions = report.MedicineConsumptions.OrderByDescending(medicine => medicine.Amount);
            return report;
        }
        [HttpPost]
        [Produces("application/json")]
        public IActionResult SendConsumptionReport(MedicineConsumptionReport report)
        {
            SftpCredentialsDTO sftpCredentials = getSftpCredentials();
            try
            {
                SaveMedicineReportToSftpServer(report, sftpCredentials);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to contact sftp server");
            }
            SendToPharmacies(report, sftpCredentials);
            /*var result = client.Post(request);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest("Pharmacy failed to receive dto");
            }*/
            return Ok("Request sent to pharmacies");
        }

        private void SendToPharmacies(MedicineConsumptionReport report, SftpCredentialsDTO sftpCredentials)
        {
            var pharmacies = _pharmacyMasterService.GetPharmacies();
            foreach (Pharmacy pharmacy in pharmacies)
            {
                RestClient client = new RestClient();
                string targetUrl = pharmacy.BaseUrl + "/api/Report/ReceiveMedicineConsumptionReport";
                RestRequest request = new RestRequest(targetUrl);
                request.AddJsonBody(new ReportDTO
                {
                    ApiKey = pharmacy.ApiKey,
                    FileName = "Report-" + report.createdDate.Ticks.ToString() + ".txt",
                    Host = sftpCredentials.Host
                });
                client.PostAsync<IActionResult>(request);
            }
        }

        private SftpCredentialsDTO getSftpCredentials()
        {
            return new SftpCredentialsDTO
            {
                Host = "192.168.0.13",
                Password = "password",
                Username = "tester"
            };
        }

        private void SaveMedicineReportToSftpServer(MedicineConsumptionReport report, SftpCredentialsDTO credentials)
        {
            string path = "MedicineReports" + Path.DirectorySeparatorChar + "Report-" +
                          report.createdDate.Ticks.ToString() + ".txt";
            try
            {
                SaveFile(report, path);
            }
            catch (Exception e)
            {
                throw e;
            }
            try
            {
                SaveToSftp(path, credentials);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        private void SaveFile(MedicineConsumptionReport consumptionReport, string path)
        {
            StreamWriter fileSaveStream = new StreamWriter(path);
            string jsonString = JsonConvert.SerializeObject(consumptionReport);
            fileSaveStream.Write(jsonString);
            fileSaveStream.Close();
        }
        private void SaveToSftp(string path, SftpCredentialsDTO credentials)
        {
            SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(credentials.Host, credentials.Username, credentials.Password));
            sftpClient.Connect();
            Stream fileStream = System.IO.File.OpenRead(path);
            string filePath = Path.GetFileName(path);
            sftpClient.UploadFile(fileStream, filePath);
            sftpClient.Disconnect();
            fileStream.Close();
        }

    }
}