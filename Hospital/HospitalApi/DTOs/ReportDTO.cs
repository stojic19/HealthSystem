using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class ReportDTO
    {
        public int DoctorsId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
