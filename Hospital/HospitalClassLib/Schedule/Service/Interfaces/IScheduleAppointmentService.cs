using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Schedule.Model.Wrappers;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IScheduleAppointmentService
    {
        public IEnumerable<DateTime> GetAvailableTermsForDoctorAndDate(int doctorId, DateTime preferredDate);

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorAndDateRange(int doctorId,DateTime startDate, DateTime endDate);

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorPriority(int doctorId,DateTime startDate, DateTime endDate);

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDatePriority(int doctorId,DateTime startDate, DateTime endDate);
    }
}
