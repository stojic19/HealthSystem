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
    public class SftpController : ControllerBase
    {
        private readonly PharmacyMasterService _pharmacyMasterService;
        private readonly MedicineConsumptionMasterService _medicineConsumptionMasterService;
        private readonly SftpMasterService _sftpMasterService;
        public SftpController(IUnitOfWork unitOfWork)
        {
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
            _sftpMasterService = new SftpMasterService();
            _medicineConsumptionMasterService = new MedicineConsumptionMasterService(unitOfWork);
        }

        [HttpPost]
        public IActionResult SendConsumptionReport(ReportRequestDTO dto)
        {
            Pharmacy pharmacy = _pharmacyMasterService.GetPharmacyById(dto.PharmacyId);
            MedicineConsumptionReport report =
                _medicineConsumptionMasterService.CreateConsumptionReportInTimeRange(dto.TimeRange);
            try
            {
                _sftpMasterService.SaveMedicineReportToSftpServer(report);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to send contact sftp server");
            }
            /*RestClient client = new RestClient();
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
                return BadRequest("Pharmacy failed to receive report");
            }*/
            return Ok();
        }
    }
}
