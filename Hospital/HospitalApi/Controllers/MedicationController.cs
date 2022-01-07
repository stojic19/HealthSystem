using Hospital.MedicalRecords.Service;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private MedicationInventoryMasterService _medicationInventoryMasterService;

        public MedicationController(IUnitOfWork unitOfWork)
        {
            _medicationInventoryMasterService = new MedicationInventoryMasterService(unitOfWork);
        }
        [HttpPost]
        public IActionResult AddMedicineQuantity(AddMedicationRequestDto medicationRequestDTO)
        {
            if (medicationRequestDTO.Quantity <= 0)
            {
                return BadRequest("Invalid quantity.");
            }
            if (medicationRequestDTO.MedicineName.Length <= 0)
            {
                return BadRequest("Invalid medicine name.");
            }
            _medicationInventoryMasterService.AddMedicineToInventory(medicationRequestDTO.MedicineName, medicationRequestDTO.Quantity);
            AddMedicationResponseDto responseDTO = new AddMedicationResponseDto() { Answer = "Succesfully added medicine." };
            return Ok(responseDTO);
        }

        [HttpPost]
        public IActionResult AddMedicineTender(TenderProcurementDTO tenderProcurementDTO)
        {
            foreach (TenderMedicineDTO medicineDto in tenderProcurementDTO.Medications)
            {
                if (medicineDto.Quantity <= 0)
                {
                    return BadRequest("Invalid quantity.");
                }
                if (medicineDto.Medicine.Name.Length <= 0)
                {
                    return BadRequest("Invalid medicine name.");
                }
                _medicationInventoryMasterService.AddMedicineToInventory(medicineDto.Medicine.Name, medicineDto.Quantity);
            }
            
            AddMedicationResponseDto responseDTO = new AddMedicationResponseDto() { Answer = "Succesfully added medicine." };
            return Ok(responseDTO);
        }
    }
}
