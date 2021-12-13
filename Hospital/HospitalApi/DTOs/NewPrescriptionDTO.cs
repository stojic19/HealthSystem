using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class NewPrescriptionDTO
    {
        [Required(ErrorMessage = "Patient id is required")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Medicine id is required")]
        public int MedicineId { get; set; }
        [Required(ErrorMessage = "Start date in which prescription is valid is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date in which prescription is valid is required")]
        public DateTime EndDate { get; set; }
    }
}
