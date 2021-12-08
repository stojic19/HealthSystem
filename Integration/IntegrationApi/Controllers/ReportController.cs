using Integration.Partnership.Model;
using Integration.Partnership.Service;
using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Renci.SshNet;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntegrationAPI.Adapters.PDF;
using IntegrationAPI.Adapters.PDF.Implementation;
using IntegrationAPI.Controllers.Base;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : BaseIntegrationController
    {
        private readonly PharmacyMasterService _pharmacyMasterService;
        private readonly MedicineConsumptionMasterService _medicineConsumptionMasterService;
        public ReportController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
            _medicineConsumptionMasterService = new MedicineConsumptionMasterService(unitOfWork);
        }

        [HttpPost]
        public MedicineConsumptionReportDTO CreateConsumptionReport(TimeRange timeRange)
        {
            var report = _medicineConsumptionMasterService.CreateConsumptionReportInTimeRange(timeRange);
            report.MedicineConsumptions = report.MedicineConsumptions.OrderByDescending(medicine => medicine.Amount);
            var reportDto = new MedicineConsumptionReportDTO
            {
                createdDate = report.createdDate,
                startDate = report.startDate,
                endDate = report.endDate,
                MedicineConsumptions = new List<MedicineConsumptionDTO>()
            };
            foreach (MedicineConsumption medicineConsumption in report.MedicineConsumptions)
            {
                reportDto.MedicineConsumptions.Add(new MedicineConsumptionDTO
                {
                    MedicineName = medicineConsumption.Medicine.Name,
                    Amount = medicineConsumption.Amount
                });
            }
            return reportDto;
        }
        [HttpPost]
        [Produces("application/json")]
        public IActionResult SendConsumptionReport(MedicineConsumptionReportDTO report)
        {
            string fileName;
            try
            {
                fileName = SaveMedicineReportToSftpServer(report, _sftpCredentials);
            }
            catch
            {
                return BadRequest("Failed to contact sftp server");
            }
            SendToPharmacies(report, fileName);
            return Ok("Report sent to pharmacies");
        }

        private void SendToPharmacies(MedicineConsumptionReportDTO report, string fileName)
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
                    FileName = fileName,
                    Host = _sftpCredentials.Host
                });
                client.PostAsync<IActionResult>(request);
            }
        }
        private string SaveMedicineReportToSftpServer(MedicineConsumptionReportDTO report, SftpCredentialsDTO credentials)
        {
            //SaveFile(report, path);
            IPDFAdapter adapter = new DynamicPDFAdapter();
            MedicineConsumptionReportToPdfDTO medicineConsumptionReportToPdfDto = new MedicineConsumptionReportToPdfDTO
            {
                StartDate = report.startDate,
                EndDate = report.endDate,
                CreatedDate = report.createdDate,
                MedicineConsumptions = report.MedicineConsumptions,
                HospitalName = _hospitalInfo.Name
            };
            string fileName = adapter.MakeMedicineConsumptionReportPdf(medicineConsumptionReportToPdfDto);
            string dest = "MedicineReports" + Path.DirectorySeparatorChar + fileName;
            SaveToSftp(dest, credentials);
            return fileName;
        }
        private void SaveFile(MedicineConsumptionReportDTO consumptionReport, string path)
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