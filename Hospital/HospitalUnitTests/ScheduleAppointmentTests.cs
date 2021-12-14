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
            Context.Specializations.Add(new Specialization()
            {
                Id = 3,
                Name = "General Practice"
            });
            Context.Doctors.Add(new Doctor()
            {
                Id = 1,
                SpecializationId = 3
            });

            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 7, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 7, 30, 00),
                    DoctorId = 1,
                });            
            
            Context.SaveChanges();

            var preferredDate = new DateTime(2021, 12, 10);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var repo = UoW.GetRepository<IScheduledEventReadRepository>();
            var scheduledEvents = repo.IsDoctorAvailableInTerm(doctor.Id, preferredDate);

            scheduledEvents.ShouldBeTrue();
        }

        [Fact]
        public void Should_return_is_available_false()
        {
            ClearDbContext();
            Context.Specializations.Add(new Specialization()
            {
                Id = 3,
                Name = "General Practice"
            });
            Context.Doctors.Add(new Doctor()
            {
                Id = 1,
                SpecializationId = 3
            });

            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 7, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 7, 30, 00),
                    DoctorId = 1,
                    IsCanceled = false
                });

            Context.SaveChanges();

            var preferredDate = new DateTime(2022, 12, 10, 7, 00, 00);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var repo = UoW.GetRepository<IScheduledEventReadRepository>();
            var scheduledEvents = repo.IsDoctorAvailableInTerm(doctor.Id, preferredDate);

            scheduledEvents.ShouldBeFalse();
        }

        [Fact]
        public void Should_return_available_appointments()
        {
            ClearDbContext();
            Context.Doctors.Add(new Doctor()
            {
                Id = 1,
                SpecializationId = 3
            });

            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 9, 13, 00, 00),
                    EndDate = new DateTime(2022, 12, 9, 13, 30, 00),
                    DoctorId = 1

                });

            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 15, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 15, 30, 00),
                    DoctorId = 1
                });

            Context.Specializations.Add(new Specialization()
            {
                Id = 3,
                Name = "General Practice"
            });

            Context.SaveChanges();

            var preferredDate = new DateTime(2021, 12, 9);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var service = new ScheduledEventService(UoW);
            var scheduledEvents = service.GetAvailableAppointments(doctor.Id, preferredDate).ToList();

            scheduledEvents.ShouldNotBeNull();
            scheduledEvents.Count().ShouldBe(16);
        }


        [Fact]
        public void Should_return_empty_available_appointments()
        {
            ClearDbContext();

            Context.Specializations.Add(new Specialization()
            {
                Id = 3,
                Name = "General Practice"
            });
            Context.Doctors.Add(new Doctor()
            {
                Id = 1,
                SpecializationId = 3
            });
            AddScheduledEvents();
            Context.SaveChanges();

            var preferredDate = new DateTime(2022, 12, 10);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var service = new ScheduledEventService(UoW);
            var scheduledEvents = service.GetAvailableAppointments(doctor.Id, preferredDate);

            scheduledEvents.ShouldBeEmpty();
            scheduledEvents.Count().ShouldBe(0);
        }

        private void AddScheduledEvents()
        {
            Context.ScheduledEvents.Add(
               new ScheduledEvent()
               {
                   StartDate = new DateTime(2022, 12, 10, 7, 00, 00),
                   EndDate = new DateTime(2022, 12, 10, 7, 30, 00),
                   DoctorId = 1
               });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 7, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 8, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 8, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 8, 30, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 8, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 9, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 9, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 9, 30, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 9, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 10, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 10, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 10, 30, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 10, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 11, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 11, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 11, 30, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 11, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 12, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 12, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 12, 30, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 12, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 13, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 13, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 13, 30, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 13, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 14, 00, 00),
                    DoctorId = 1
                });
            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 14, 00, 00),
                    EndDate = new DateTime(2022, 12, 10, 14, 30, 00),
                    DoctorId = 1
                });

            Context.ScheduledEvents.Add(
                new ScheduledEvent()
                {
                    StartDate = new DateTime(2022, 12, 10, 14, 30, 00),
                    EndDate = new DateTime(2022, 12, 10, 15, 00, 00),
                    DoctorId = 1
                });
        }
    }
}
