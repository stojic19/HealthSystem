using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IScheduleAppointmentService
    {
        public IEnumerable<DateTime> GetAvailableTermsForDoctorAndDate(int doctorId, DateTime preferredDate);
    }
}
