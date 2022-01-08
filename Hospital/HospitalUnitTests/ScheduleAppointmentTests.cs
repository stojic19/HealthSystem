using System;
using System.Linq;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository;
using HospitalUnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace HospitalUnitTests
{
    public class ScheduleAppointmentTests : BaseTest
    {
        public ScheduleAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Should_return_is_available_true()
        {
            ClearDbContext();
            var doctor = new Doctor(1, 0, new Specialization("General Practice", ""));
            Context.Doctors.Add(doctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 00, 00),
                new DateTime(2022, 12, 10, 7, 30, 00), new DateTime(), 0, 1, doctor));
            Context.SaveChanges();

            var preferredDate = new DateTime(2022, 12, 10);

            var dr = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var repo = UoW.GetRepository<IDoctorReadRepository>();
            var isDoctorAvailable = dr != null && repo.IsDoctorAvailableInTerm(dr.Id, preferredDate);

            isDoctorAvailable.ShouldBeTrue();
        }

        [Fact]
        public void Should_return_is_available_false()
        {
            ClearDbContext();
            var doctor = new Doctor(1, 0, new Specialization("General Practice", ""));
            Context.Doctors.Add(doctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 00, 00),
                new DateTime(2022, 12, 10, 7, 30, 00), new DateTime(), 0, 1, doctor));
            Context.SaveChanges();
            
            var preferredDate = new DateTime(2022, 12, 10, 7, 00, 00);

            var dr = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var repo = UoW.GetRepository<IDoctorReadRepository>();
            var isDoctorAvailable = repo.IsDoctorAvailableInTerm(dr.Id, preferredDate);

            isDoctorAvailable.ShouldBeFalse();
        }

        [Fact]
        public void Should_return_available_appointments()
        {
            ClearDbContext();
            Context.Shifts.Add(new Shift(1, "First Shift", 7, 15));
            var dr = new Doctor(1, 1, new Specialization("General Practice", ""));
            Context.Doctors.Add(dr);
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 9, 13, 00, 00),
                new DateTime(2022, 12, 9, 13, 30, 00), new DateTime(), 0, 1, dr));
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 15, 00, 00),
                new DateTime(2022, 12, 10, 15, 30, 00), new DateTime(), 0, 1, dr));
            Context.SaveChanges();

            var preferredDate = new DateTime(2021, 12, 9);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));
            
            var service = new ScheduleAppointmentService(UoW);
            var availableTerms = service.GetAvailableTermsForDoctorAndDate(doctor.Id, preferredDate).ToList();

            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(16);
        }


        [Fact]
        public void Should_return_empty_available_appointments()
        {
            ClearDbContext();
            Context.Shifts.Add(new Shift(1, "First Shift", 7, 15));
            var dr = new Doctor(1, 1, new Specialization("General Practice", ""));
            Context.Doctors.Add(dr);
            AddScheduledEvents(dr);
            Context.SaveChanges();

            var preferredDate = new DateTime(2022, 12, 10);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var service = new ScheduleAppointmentService(UoW);
            var availableTerms = service.GetAvailableTermsForDoctorAndDate(doctor.Id, preferredDate).ToList();

            availableTerms.ShouldBeEmpty();
            availableTerms.Count.ShouldBe(0);
        }

        private void AddScheduledEvents(Doctor dr)
        {
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 00, 00),
                new DateTime(2022, 12, 10, 7, 30, 00), new DateTime(), 0, 1, dr));
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 30, 00),
                new DateTime(2022, 12, 10, 8, 00, 00), new DateTime(), 0, 1, dr));
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 8, 00, 00),
                 new DateTime(2022, 12, 10, 8, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 8, 30, 00),
                 new DateTime(2022, 12, 10, 9, 00, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 9, 00, 00),
                 new DateTime(2022, 12, 10, 9, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 9, 30, 00),
                 new DateTime(2022, 12, 10, 10, 00, 00),  new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 10, 00, 00),
                 new DateTime(2022, 12, 10, 10, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 10, 30, 00),
                 new DateTime(2022, 12, 10, 11, 00, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 11, 00, 00),
                 new DateTime(2022, 12, 10, 11, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 11, 30, 00),
                 new DateTime(2022, 12, 10, 12, 00, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 12, 00, 00),
                 new DateTime(2022, 12, 10, 12, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 12, 30, 00),
                 new DateTime(2022, 12, 10, 13, 00, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 13, 00, 00),
                 new DateTime(2022, 12, 10, 13, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 13, 30, 00),
                 new DateTime(2022, 12, 10, 14, 00, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 14, 00, 00),
                 new DateTime(2022, 12, 10, 14, 30, 00), new DateTime(), 0, 1, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 14, 30, 00),
                 new DateTime(2022, 12, 10, 15, 00, 00), new DateTime(), 0, 1, dr));
        }
    }
}
