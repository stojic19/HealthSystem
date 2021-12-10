using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class TimePeriodDTO
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
