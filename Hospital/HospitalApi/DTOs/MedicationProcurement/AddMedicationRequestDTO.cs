using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class AddMedicationRequestDTO
    {
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
    }
}
