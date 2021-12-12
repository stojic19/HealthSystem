using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class PrescriptionToIntegrationDTO
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string MedicineName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
