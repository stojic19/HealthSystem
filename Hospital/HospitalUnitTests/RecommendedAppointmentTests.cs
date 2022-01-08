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
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Service.Interfaces;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Model.Wrappers;
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
            Context.Rooms.Add(new Room{Id = 1, Name = "Ordination"});
            Context.Shifts.Add(new Shift(3, "first", 7, 15));
            var firstDoctor = new Doctor(1, 3, new Specialization("General Practice", ""));
            Context.Doctors.Add(firstDoctor);
            var secondDoctor = new Doctor(2, 3, new Specialization("General Practice", ""));
            Context.Doctors.Add(secondDoctor);
            Context.SaveChanges();

            var service = new ScheduleAppointmentService(UoW);
            var appointments = service.GetAvailableAppointmentsForDoctorAndDateRange(1, new TimePeriod(new DateTime(2023, 12, 16, 9, 0, 0), new DateTime(2023, 12, 17, 17, 0, 0))).ToList();

            appointments.Count.ShouldNotBe(0);
            appointments[0].Doctor.Id.ShouldBe(1);
        }

        [Fact]
        public void Should_be_eight_available_appointments()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room { Id = 1, Name = "Ordination" });
            Context.Shifts.Add(new Shift(3, "second", 13, 17));
            var firstDoctor = new Doctor(1, 3, new Specialization("General Practice", ""));
            Context.Doctors.Add(firstDoctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment,false,false, new DateTime(2023, 12, 16, 13, 00, 00),
                new DateTime(2023, 12, 16, 14, 00, 00),new DateTime(),0,1,firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 17, 15, 00, 00),
                new DateTime(2023, 12, 17, 16, 00, 00), new DateTime(), 0, 1, firstDoctor));
            Context.SaveChanges();

            var service = new ScheduleAppointmentService(UoW);
            var appointments = service.GetAvailableAppointmentsForDoctorAndDateRange(1, new TimePeriod(new DateTime(2023, 12, 16, 13, 0, 0), new DateTime(2023, 12, 16, 17, 0, 0))).ToList();

            appointments.Count.ShouldBe(8);

        }


        //[Fact]
        //public void Should_be_doctors_id_1()
        //{
        //    ClearDbContext();
        //    Context.Specializations.Add(new Specialization
        //    {
        //        Id = 1,
        //        Name = "GP"
        //    });
        //    Context.Doctors.Add(new Doctor
        //    {
        //        SpecializationId = 1,
        //        Id = 1,
        //        FirstName = "Marija",
        //        LastName = "Savic",
        //        UserName = "marija123"
        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2023, 12, 16, 13, 00, 00),
        //        EndDate = new DateTime(2023, 12, 16, 14, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2023, 12, 16, 14, 00, 00),
        //        EndDate = new DateTime(2023, 12, 16, 15, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.SaveChanges();
        //    RecommendedAppointmentService service = new RecommendedAppointmentService(UoW, Context);
        //    var appointments = service.GetAvailableAppointmentsForDoctorAndDateRange(1, new DateTime(2023, 12, 16, 12, 0, 0), new DateTime(2023, 12, 16, 17, 0, 0)).ToList();

        //    appointments[0].Doctor.Id.ShouldBe(1);

        //}

        //[Fact]
        //public void Available_appointments_when_doctor_is_priority()
        //{
        //    ClearDbContext();
        //    Context.Specializations.Add(new Specialization
        //    {
        //        Id = 1,
        //        Name = "GP"
        //    });
        //    Context.Doctors.Add(new Doctor
        //    {
        //        SpecializationId = 1,
        //        Id = 1,
        //        FirstName = "Marija",
        //        LastName = "Savic",
        //        UserName = "marija123"
        //    });
        //    Context.Doctors.Add(new Doctor
        //    {
        //        SpecializationId = 1,
        //        Id = 2,
        //        FirstName = "Sanja",
        //        LastName = "Miranic"
        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 7, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23,8, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 8, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 9, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 9, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 10, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 10, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 11, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 11, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 12, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 12, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 13, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 13, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 14, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 14, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 15, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 15, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 16, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 16, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 17, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 17, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 18, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 18, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 19, 00, 00),
        //        DoctorId = 1

        //    });




        //    Context.SaveChanges();
        //    var service = new RecommendedAppointmentService(UoW, Context);
        //    var appointments = service.GetAvailableAppointmentsForDoctorPriority(1, new DateTime(2021, 12, 23, 0, 0, 0), new DateTime(2021, 12, 24, 0, 0, 0)).ToList();

        //    appointments[0].StartDate.Date.ShouldBeLessThan(new DateTime(2021, 12, 23));

        //}

        //[Fact]
        //public void Available_appointments_when_date_is_priority()
        //{
        //    ClearDbContext();
        //    Context.Specializations.Add(new Specialization
        //    {
        //        Id = 1,
        //        Name = "GP"
        //    });
        //    Context.Doctors.Add(new Doctor
        //    {
        //        SpecializationId = 1,
        //        Id = 1,
        //        FirstName = "Marija",
        //        LastName = "Savic",
        //        UserName = "marija123"
        //    });
        //    Context.Doctors.Add(new Doctor
        //    {
        //        SpecializationId = 1,
        //        Id = 2,
        //        FirstName = "Sanja",
        //        LastName = "Miranic",
        //        UserName = "sanjaMir123"
        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 7, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 8, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 8, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 9, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 9, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 10, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 10, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 11, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 11, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 12, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 12, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 13, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 13, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 14, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 14, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 15, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 15, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 16, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 16, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 17, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 17, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 18, 00, 00),
        //        DoctorId = 1

        //    });
        //    Context.ScheduledEvents.Add(new ScheduledEvent
        //    {
        //        StartDate = new DateTime(2021, 12, 23, 18, 00, 00),
        //        EndDate = new DateTime(2021, 12, 23, 19, 00, 00),
        //        DoctorId = 1

        //    });

        //    Context.SaveChanges();
        //    var service = new RecommendedAppointmentService(UoW, Context);
        //    var appointments = service.GetAvailableAppointmentsForDatePriority(1, new DateTime(2021, 12, 23, 0, 0, 0),
        //        new DateTime(2021, 12, 24, 0, 0, 0)).ToList();

        //    appointments[0].Doctor.Id.ShouldBe(2);

        //}
    }
}
