using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class ScheduledEventsDTO
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

       
    }
}
