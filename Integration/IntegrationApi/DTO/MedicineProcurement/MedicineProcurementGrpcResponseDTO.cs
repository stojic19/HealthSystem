using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class MedicineProcurementGrpcResponseDto
    {
        public bool ConnectionSuccesfull { get; set; }

        public MedicineProcurementResponseDto Response { get; set; }
    }
}
