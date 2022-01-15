using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using HospitalApi.DTOs.ConsumptionReport;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicationExpenditureReportController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MedicationExpenditureReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult GetMedicationExpenditureReport(TimeRangeDto timePeriod)
        {
            if (timePeriod.EndTime == DateTime.MinValue || timePeriod.StartTime == DateTime.MinValue || timePeriod.StartTime > timePeriod.EndTime)
            {
                return BadRequest("Wrong time period!");
            }
            MedicationConsumptionReport report = new MedicationConsumptionReportService(_unitOfWork)
                .CreateMedicationExpenditureReportInTimePeriod(new TimePeriod(timePeriod.StartTime, timePeriod.EndTime));
            var reportDto = new MedicationExpenditureReportDTO
            {
                CreatedDate = report.CreatedDate,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                MedicationExpenditureDTO = new List<MedicationExpenditureDTO>()
            };
            foreach (MedicationConsumption medicineConsumption in report.MedicationConsumptions)
            {
                reportDto.MedicationExpenditureDTO.Add(new MedicationExpenditureDTO
                {
                    MedicineName = medicineConsumption.Medication.Name,
                    Amount = medicineConsumption.Amount
                });
            }
            return Ok(reportDto);
        }
    }
}
