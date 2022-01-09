using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class ShiftDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int From { get; set; }
        public int To { get; set; }

    }
}
