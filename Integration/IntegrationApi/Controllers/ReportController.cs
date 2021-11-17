using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Integration.MasterServices;
using Integration.Model;
using Integration.Repositories.Base;
using IntegrationAPI.DTO;
using RestSharp;

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
        public IActionResult SendConsumptionReport(ReportRequestDTO dto)
        {
            MedicineConsumptionReport report = dto.MedicineConsumptionReport;
            try
            {
                _sftpMasterService.SaveMedicineReportToSftpServer(report);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to contact sftp server");
            }
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(dto.PharmacyId);
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/Sftp/ReceiveRequest";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(new SendReportDTO
            {
                ApiKey = pharmacy.ApiKey.ToString(),
                FileName = "Report-" + report.createdDate.Ticks.ToString() + ".txt"
            });
            var result = client.Post(request);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest("Pharmacy failed to receive dto");
            }
            return Ok();
        }
    }
}
/*[HttpPost]
       public IActionResult SendConsumptionReport(ReportRequestDTO dto)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(dto.PharmacyId);
            MedicineConsumptionReport dto =
                _medicineConsumptionMasterService.CreateConsumptionReportInTimeRange(dto.TimeRange);
            try
            {
                _sftpMasterService.SaveMedicineReportToSftpServer(dto);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to contact sftp server");
            }
            RestClient client = new RestClient();
            string targetUrl = pharmacy.BaseUrl + "/api/Sftp/ReceiveRequest";
            RestRequest request = new RestRequest(targetUrl);
            request.AddJsonBody(new SendReportDTO
            {
                ApiKey = pharmacy.ApiKey.ToString(),
                FileName = "Report-" + dto.createdDate.Ticks.ToString() + ".txt"
            });
            var result = client.Post(request);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest("Pharmacy failed to receive dto");
            }
            return Ok();
        }*/