using Hospital.Database.EfStructures;
using Hospital.Schedule.Model;
using Hospital.Schedule.Model.Wrappers;
using Hospital.SharedModel.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Service
{
    public class RecommendedAppointmentService
    {
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;
        private const int StartHour = 7;
        private const int EndHour = 19;
        public RecommendedAppointmentService(IUnitOfWork uow, AppDbContext context)
        {
            _context = context;
            _uow = uow;
        }

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorAndDateRange(int doctorId, DateTime startDate, DateTime endDate)
        {
            var allAppointments = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddHours(1))
            {
                if (date.Hour is >= StartHour and < EndHour)
                {
                    allAppointments.Add(date);
                }
            }
            var scheduledEvents = _context.Set<ScheduledEvent>().AsEnumerable();
            var scheduledAppointments = (from appointment in allAppointments
                                         from scheduledEvent in scheduledEvents
                                         where (scheduledEvent.DoctorId == doctorId && scheduledEvent.IsCanceled!=true && DateTime.Compare(scheduledEvent.StartDate, appointment) == 0)
                                         select appointment);
            var dateTimes = allAppointments.Except(scheduledAppointments);
            return AvailableAppointments(dateTimes, doctorId);
        }

        private IEnumerable<AvailableAppointment> AvailableAppointments(IEnumerable<DateTime> allAppointments, int doctorId)
        {
            return allAppointments.Select(sa => new AvailableAppointment
            {
                Doctor = _uow.GetRepository<IDoctorReadRepository>()
                                                .GetAll().Include(x => x.Specialization).First(x=>x.Id==doctorId),
                StartDate = sa
            }).ToList();
        }

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDoctorPriority(int doctorId, DateTime startDate, DateTime endDate)
        {
            var allAppointments = new List<DateTime>();
            for (var date = startDate.AddDays(-5); date <= endDate.AddDays(5); date = date.AddHours(1))
            {
                if (date.Hour is >= StartHour and < EndHour)
                {
                    allAppointments.Add(date);
                }
            }
            var scheduledEvents = _context.Set<ScheduledEvent>().AsEnumerable();
            var scheduledAppointments = (from appointment in allAppointments
                                         from scheduledEvent in scheduledEvents
                                         where (scheduledEvent.DoctorId == doctorId && scheduledEvent.IsCanceled != true && DateTime.Compare(scheduledEvent.StartDate, appointment) == 0)
                                         select appointment);
            var dateTimes = allAppointments.Except(scheduledAppointments);
            return AvailableAppointments(dateTimes, doctorId);
        }

        public IEnumerable<AvailableAppointment> GetAvailableAppointmentsForDatePriority(int doctorId, DateTime startDate, DateTime endDate)
        {
            var availableAppointments = new List<AvailableAppointment>();
            var doctorRepo = _uow.GetRepository<IDoctorReadRepository>();
            var firstDoctor = doctorRepo.GetById(doctorId);
            var specializationId = firstDoctor.SpecializationId;
            foreach (var doctor in doctorRepo.GetDoctorsBySpecialization(specializationId).ToList())
            {
                foreach (var appointment in GetAvailableAppointmentsForDoctorAndDateRange(doctor.Id, startDate, endDate))
                {
                    availableAppointments.Add(appointment);
                }
            }
            return availableAppointments;
        }
    }
}
