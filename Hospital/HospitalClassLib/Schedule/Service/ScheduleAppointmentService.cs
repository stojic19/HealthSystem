using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Database.EfStructures;
using Hospital.Schedule.Model.Wrappers;
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

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorAndDateRange(int doctorId, DateTime startDate, DateTime endDate)
        {
            var allAppointments = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                allAppointments.AddRange(GetAvailableTermsForDoctorAndDate(doctorId, date));
            }
            return AvailableAppointments(allAppointments, doctorId);
        }

        private IEnumerable<AvailableAppointment> AvailableAppointments(IEnumerable<DateTime> allAppointments, int doctorId)
        {
            return allAppointments.Select(sa => new AvailableAppointment
            {
                Doctor = _uow.GetRepository<IDoctorReadRepository>()
                                                .GetAll().Include(x => x.Specialization).First(x => x.Id == doctorId),
                StartDate = sa
            }).ToList();
        }

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorPriority(int doctorId, DateTime startDate, DateTime endDate)
        {
            var allAppointments = new List<DateTime>();
            for (var date = startDate.AddDays(-3); date <= endDate.AddDays(3); date = date.AddDays(1))
            {
                allAppointments.AddRange(GetAvailableTermsForDoctorAndDate(doctorId, date));
            }
            return AvailableAppointments(allAppointments, doctorId);
        }

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDatePriority(int doctorId, DateTime startDate, DateTime endDate)
        {
            var doctorRepo = _uow.GetRepository<IDoctorReadRepository>();
            var firstDoctor = doctorRepo.GetById(doctorId);
            var specializationName = firstDoctor.Specialization.Name;
            return doctorRepo.GetSpecializedDoctors(specializationName).ToList().
                                SelectMany(doctor => GetAvailableAppointmentsForDoctorAndDateRange(doctor.Id, startDate, endDate))
                                .ToList();
        }
    }
}
