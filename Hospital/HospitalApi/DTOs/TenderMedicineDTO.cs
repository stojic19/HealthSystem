using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs
{
    public class TenderMedicineDTO
    {
        public MedicineDTO Medicine { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Quantity { get; set; }
    }
}
