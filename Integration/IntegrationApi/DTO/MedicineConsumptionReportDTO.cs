using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineConsumptionReportDTO
    {
        [Required(ErrorMessage = "Start date is necessary!")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "End date is necessary!")]
        public DateTime endDate { get; set; }
        [Required(ErrorMessage = "Date on which this report is created is necessary!")]
        public DateTime createdDate { get; set; }
        [Required(ErrorMessage = "List of consumed medicine is necessary!")]
        public List<MedicineConsumptionDTO> MedicineConsumptions { get; set; }
    }
}
