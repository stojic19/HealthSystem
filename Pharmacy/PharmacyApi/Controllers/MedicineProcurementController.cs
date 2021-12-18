using Microsoft.AspNetCore.Mvc;
using Pharmacy.Exceptions;
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
        public IActionResult CheckMedicineAvailability(CheckMedicineAvailabilityRequestDTO requestDTO)
        {
            try
            {
                _hospitalAuthService.ValidateApiKey(requestDTO.ApiKey);
                bool answer = _medicineProcurementService.IsMedicineAvailable(requestDTO.MedicineName, requestDTO.ManufacturerName, requestDTO.Quantity);

                return Ok(new CheckMedicineAvailabilityResponseDTO() { Answer = answer });
            }
            catch (UnauthorizedAccessException exception)
            {
                return Unauthorized(exception.Message);
            } 
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost("execute")]
        public IActionResult ExecuteMedicineProcurement(MedicineProcurementRequestDTO requestDTO)
        {
            try
            {
                _hospitalAuthService.ValidateApiKey(requestDTO.ApiKey);
                _medicineProcurementService.ExecuteProcurement(requestDTO.MedicineName, requestDTO.ManufacturerName, requestDTO.Quantity);

                return Ok("Procurement executed successfully.");
            }
            catch (UnauthorizedAccessException exception)
            {
                return Unauthorized(exception.Message);
            }
            catch (MedicineFromManufacturerNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (MedicineUnavailableException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
