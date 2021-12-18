using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class AvailableAppointmentDTO
    {
        public DateTime StartDate { get; set; }
        public DoctorAppointmentDTO Doctor { get; set; }
    }
}
