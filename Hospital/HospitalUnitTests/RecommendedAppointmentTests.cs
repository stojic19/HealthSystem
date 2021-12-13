using Hospital.Schedule.Model;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalUnitTests
{
    public class RecommendedAppointmentTests : BaseTest
    {
        public RecommendedAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Available_appointments_should_not_be_zero()
        {
            ClearDbContext();
            Context.Specializations.Add(new Specialization
            {
                Id = 1,
                Name = "GP"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 1,
                FirstName = "Marija",
                LastName = "Savic",
                UserName = "marija123"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 2,
                FirstName = "Sanja",
                LastName = "Miranic"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 13, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 14, 00, 00),
                DoctorId = 1

            });

            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 15, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 16, 00, 00),
                DoctorId = 1

            });

            Context.SaveChanges();
            var service = new RecommendedAppointmentService(UoW, Context);
            var appointments = service.GetAvailableAppointmentsForDoctorAndDateRange(1, new DateTime(2023, 12, 16, 9, 0, 0), new DateTime(2023, 12, 17, 17, 0, 0)).ToList();

            appointments.Count().ShouldNotBe(0);

        }

        [Fact]
        public void Should_be_three_available_appointments()
        {
            ClearDbContext();
            Context.Specializations.Add(new Specialization
            {
                Id = 1,
                Name = "GP"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 1,
                FirstName = "Marija",
                LastName = "Savic",
                UserName = "marija123"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 13, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 14, 00, 00),
                DoctorId = 1

            });

            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 17, 15, 00, 00),
                EndDate = new DateTime(2023, 12, 17, 16, 00, 00),
                DoctorId = 1

            });

            Context.SaveChanges();
            var service = new RecommendedAppointmentService(UoW, Context);
            var appointments = service.GetAvailableAppointmentsForDoctorAndDateRange(1, new DateTime(2023, 12, 16, 13, 0, 0), new DateTime(2023, 12, 16, 17, 0, 0)).ToList();

            appointments.Count().ShouldBe(3);

        }


        [Fact]
        public void Should_be_doctors_id_1()
        {
            ClearDbContext();
            Context.Specializations.Add(new Specialization
            {
                Id = 1,
                Name = "GP"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 1,
                FirstName = "Marija",
                LastName = "Savic",
                UserName = "marija123"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 13, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 14, 00, 00),
                DoctorId = 1

            });

            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 14, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 15, 00, 00),
                DoctorId = 1

            });

            Context.SaveChanges();
            RecommendedAppointmentService service = new RecommendedAppointmentService(UoW, Context);
            var appointments = service.GetAvailableAppointmentsForDoctorAndDateRange(1, new DateTime(2023, 12, 16, 12, 0, 0), new DateTime(2023, 12, 16, 17, 0, 0)).ToList();

            appointments[0].Doctor.Id.ShouldBe(1);

        }

        [Fact]
        public void Available_appointments_when_doctor_is_priority()
        {
            ClearDbContext();
            Context.Specializations.Add(new Specialization
            {
                Id = 1,
                Name = "GP"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 1,
                FirstName = "Marija",
                LastName = "Savic",
                UserName = "marija123"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 2,
                FirstName = "Sanja",
                LastName = "Miranic"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 13, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 14, 00, 00),
                DoctorId = 1

            });

            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 15, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 16, 00, 00),
                DoctorId = 1

            });

            Context.SaveChanges();
            var service = new RecommendedAppointmentService(UoW, Context);
            var appointments = service.GetAvailableAppointmentsForDoctorPriority(1, new DateTime(2023, 12, 16, 13, 0, 0), new DateTime(2023, 12, 16, 17, 0, 0)).ToList();

            appointments[0].StartDate.Date.ShouldBeLessThan(new DateTime(2023, 12, 16));

        }

        [Fact]
        public void Available_appointments_when_date_is_priority()
        {
            ClearDbContext();
            Context.Specializations.Add(new Specialization
            {
                Id = 1,
                Name = "GP"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 1,
                FirstName = "Marija",
                LastName = "Savic",
                UserName = "marija123"
            });
            Context.Doctors.Add(new Doctor
            {
                SpecializationId = 1,
                Id = 2,
                FirstName = "Sanja",
                LastName = "Miranic",
                UserName = "sanjaMir123"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 13, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 14, 00, 00),
                DoctorId = 1

            });

            Context.ScheduledEvents.Add(new ScheduledEvent
            {
                StartDate = new DateTime(2023, 12, 16, 14, 00, 00),
                EndDate = new DateTime(2023, 12, 16, 15, 00, 00),
                DoctorId = 1

            });

            Context.SaveChanges();
            var service = new RecommendedAppointmentService(UoW, Context);
            var appointments = service.GetAvailableAppointmentsForDatePriority(1, new DateTime(2023, 12, 16, 13, 0, 0),
                new DateTime(2023, 12, 16, 15, 0, 0)).ToList();

            appointments[0].Doctor.Id.ShouldBe(2);

        }
    }
}
