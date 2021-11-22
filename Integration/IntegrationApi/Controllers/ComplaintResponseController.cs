using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.MasterServices;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using IntegrationAPI.Adapters;
using IntegrationAPI.DTO;

namespace IntegrationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplaintResponseController : ControllerBase
    {
        private ComplaintResponseMasterService _complaintResponseMasterService;
        private PharmacyMasterService _pharmacyMasterService;
        private IUnitOfWork _unitOfWork;

        public ComplaintResponseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _complaintResponseMasterService = new ComplaintResponseMasterService(unitOfWork);
            _pharmacyMasterService = new PharmacyMasterService(unitOfWork);
        }

        [HttpPost]
        public IActionResult ReceiveComplaintResponse(ComplaintResponseDTO complaintResponseDTO)
        {
            Pharmacy pharmacy = _pharmacyMasterService.FindPharmacyByApiKey(complaintResponseDTO.ApiKey.ToString());
            if (pharmacy == null) return BadRequest("Pharmacy not registered");
            Complaint complaint = _unitOfWork.GetRepository<IComplaintReadRepository>().GetAll()
                .FirstOrDefault(x => x.CreatedDate == complaintResponseDTO.ComplaintCreatedDate);
            if (complaint == null) return BadRequest("Complaint not found");
            ComplaintResponse complaintResponse = new ComplaintResponse
            {
                Text = complaintResponseDTO.Text,
                CreatedDate = complaintResponseDTO.CreatedDate,
                ComplaintId = complaint.Id
            };
            _complaintResponseMasterService.SaveComplaintResponse(complaintResponse);

            return Ok("Complaint response received!");
        }
    }
}
