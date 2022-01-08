using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Model.Wrappers;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IScheduleAppointmentService
    {
        public IEnumerable<DateTime> GetAvailableTermsForDoctorAndDate(int doctorId, DateTime preferredDate);

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorAndDateRange(int doctorId,TimePeriod timePeriod);

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorPriority(int doctorId,TimePeriod timePeriod);

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDatePriority(int doctorId,TimePeriod timePeriod);
    }
}
