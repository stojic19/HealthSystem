using Microsoft.AspNetCore.Mvc;
using Pharmacy.Repositories.Base;
using Pharmacy.Services;
using PharmacyApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineProcurementController : Controller
    {
        private readonly HospitalAuthService _hospitalAuthService;
        private readonly MedicineProcurementService _medicineProcurementService;

        public MedicineProcurementController(IUnitOfWork uow)
        {
            _hospitalAuthService = new HospitalAuthService(uow);
            _medicineProcurementService = new MedicineProcurementService(uow);
        }

        [HttpPost("check")]
        public IActionResult CheckMedicineAvailability(CheckMedicineAvailabilityRequestDTO checkMedicineAvailabilityRequestDTO)
        {
            try
            {
                _hospitalAuthService.ValidateApiKey(checkMedicineAvailabilityRequestDTO.ApiKey);
                bool isMedicineAvailable = _medicineProcurementService
                    .IsMedicineAvailable(checkMedicineAvailabilityRequestDTO.MedicineName,
                    checkMedicineAvailabilityRequestDTO.ManufacturerName, checkMedicineAvailabilityRequestDTO.Quantity);

                return Ok(new CheckMedicineAvailabilityResponseDTO() { Answer = isMedicineAvailable });
            }
            catch (UnauthorizedAccessException uae)
            {
                return Unauthorized(uae.Message);
            } 
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
    }
}
