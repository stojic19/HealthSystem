using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class CheckMedicineAvailabilityGrpcResponseDto
    {
        public bool ConnectionSuccesfull { get; set; }
        public string ExceptionMessage { get; set; }
        public CheckMedicineAvailabilityResponseDto Response { get; set; }
    }
}
