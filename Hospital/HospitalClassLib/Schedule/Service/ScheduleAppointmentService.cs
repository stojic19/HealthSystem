using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service.Interfaces;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Schedule.Service
{
    public class ScheduleAppointmentService : IScheduleAppointmentService
    {
        private readonly IUnitOfWork _uow;
        private const int StartHour = 7;
        private const int EndHour = 15;
        private const double TermDurationInHours = 0.5;
        public ScheduleAppointmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IEnumerable<DateTime> GetAvailableTermsForDoctorAndDate(int doctorId, DateTime preferredDate)
        {
            var doctorsShift = _uow.GetRepository<IShiftReadRepository>().GetShiftForDoctor(doctorId);
            var preferredDay = new TimePeriod(preferredDate.AddHours(doctorsShift.From), preferredDate.AddHours(doctorsShift.To));
            var allTerms = preferredDay.SlotsWithDuration(TermDurationInHours);

            return (from term in allTerms
                where _uow.GetRepository<IDoctorReadRepository>().IsDoctorAvailableInTerm(doctorId, term.StartTime)
                select term.StartTime).ToList();
        }
    }
}
