using Integration.Pharmacies.Model;
using Integration.Pharmacies.Repository;
using Integration.Pharmacies.Service;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using IntegrationAPI.DTO.Complaints;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using System;

namespace IntegrationAPI.Controllers.Complaints
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

            Notification notification = new Notification {
                Title = "New complaint response",
                Description = "New response has been recieved for complaint:" + complaint.Title,
                CreatedDate = DateTime.Now,
            };
            _unitOfWork.GetRepository<INotificationWriteRepository>().Add(notification);

            return Ok("Complaint response received!");
        }
    }
}
