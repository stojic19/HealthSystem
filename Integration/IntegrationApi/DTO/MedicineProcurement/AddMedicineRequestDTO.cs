using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class AddMedicineRequestDto
    {
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
    }
}
