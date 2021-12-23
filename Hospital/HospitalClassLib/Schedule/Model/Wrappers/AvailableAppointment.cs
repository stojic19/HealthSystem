using Hospital.SharedModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model.Wrappers
{
    public class AvailableAppointment
    {
        public DateTime StartDate { get; set; }
        public Doctor Doctor { get; set; }
    }
}
