using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO.MedicineConsumption
{
    public class MedicineConsumptionReportDTO
    {
        [Required(ErrorMessage = "Start date is necessary!")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "End date is necessary!")]
        public DateTime endDate { get; set; }
        [Required(ErrorMessage = "Date on which this report is created is necessary!")]
        public DateTime createdDate { get; set; }
        [Required(ErrorMessage = "List of medications is necessary!")]
        public List<MedicationExpenditureDTO> MedicationExpenditureDTO { get; set; }
    }
}
