using Hospital.Schedule.Model;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Linq;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Model.Wrappers;
using Xunit;

namespace HospitalUnitTests
{
    public class RecommendedAppointmentTests : BaseTest
    {
        private readonly ScheduleAppointmentService _scheduleAppointmentService;

        public RecommendedAppointmentTests(BaseFixture fixture) : base(fixture)
        {
            _scheduleAppointmentService = new(UoW);
        }

        [Fact]
        public void Available_appointments_should_not_be_zero() 
        {
            #region Arrange
            ClearDbContext();
            Context.Rooms.Add(new Room{Id = 1, Name = "Ordination"});
            Context.Shifts.Add(new Shift(3, "first", 7, 15));
            var testDoctor = new Doctor(1, 3, new Specialization("General Practice", ""));
            Context.Doctors.Add(testDoctor);
            Context.SaveChanges();
            #endregion

            var appointments = _scheduleAppointmentService.GetAvailableAppointmentsForDoctorAndDateRange(1, new TimePeriod(new DateTime(2023, 12, 16), new DateTime(2023, 12, 16))).ToList();

            appointments.Count.ShouldNotBe(0);
        }

        [Fact]
        public void Should_be_six_available_appointments()
        {
            #region Arrange

            ClearDbContext();
            var room = new Room {Id = 1, Name = "Ordination"};
            Context.Rooms.Add(room);
            Context.Shifts.Add(new Shift(3, "second", 13, 17));
            var firstDoctor = new Doctor(1, 3, new Specialization("General Practice", "")) {Room = room};
            Context.Doctors.Add(firstDoctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment,false,false, new DateTime(2023, 12, 16, 13, 0, 0),
                new DateTime(2023, 12, 16, 13, 30, 0),new DateTime(),1,1,firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 15, 0, 0),
                new DateTime(2023, 12, 16, 15, 30, 0), new DateTime(), 1, 1, firstDoctor));
            Context.SaveChanges();
            #endregion

            var appointments = _scheduleAppointmentService.GetAvailableAppointmentsForDoctorAndDateRange(1, new TimePeriod(new DateTime(2023, 12, 16), new DateTime(2023, 12, 16))).ToList();
           
            appointments.Count.ShouldBe(6);

        }

        [Fact]
        public void Should_be_doctors_id_2()
        {
            #region Arrange

            ClearDbContext();
            var room = new Room { Id = 1, Name = "Ordination" };
            Context.Rooms.Add(room);
            Context.Shifts.Add(new Shift(3, "second", 13, 17));
            var firstDoctor = new Doctor(2, 3, new Specialization("General Practice", "")) {Room = room};
            Context.Doctors.Add(firstDoctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 13, 0, 0),
                new DateTime(2023, 12, 16, 13, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 17, 15, 0, 0),
                new DateTime(2023, 12, 17, 15, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.SaveChanges();
            #endregion

            var appointments = _scheduleAppointmentService.GetAvailableAppointmentsForDoctorAndDateRange(2, new TimePeriod(new DateTime(2023, 12, 16), new DateTime(2023, 12, 16))).ToList();

            appointments[0].Doctor.Id.ShouldBe(2);

        }

        [Fact]
        public void Available_appointments_when_doctor_is_priority()
        {
            #region Arrange

            ClearDbContext();
            var room = new Room { Id = 1, Name = "Ordination" };
            Context.Rooms.Add(room);
            Context.Shifts.Add(new Shift(3, "second", 13, 17));
            var firstDoctor = new Doctor(1, 3, new Specialization("General Practice", "")) { Room = room };
            Context.Doctors.Add(firstDoctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 13, 0, 0),
                new DateTime(2023, 12, 16, 13, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 13, 30, 0),
                new DateTime(2023, 12, 16, 14, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 14, 0, 0),
                new DateTime(2023, 12, 16, 14, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 14, 30, 0),
                new DateTime(2023, 12, 16, 15, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 15, 0, 0),
                new DateTime(2023, 12, 16, 15, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 15, 30, 0),
                new DateTime(2023, 12, 16, 16, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 16, 0, 0),
                new DateTime(2023, 12, 16, 16, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 16, 30, 0),
                new DateTime(2023, 12, 16, 17, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.SaveChanges();
            #endregion

            var appointments = _scheduleAppointmentService.GetAvailableAppointmentsForDoctorPriority(1, new TimePeriod(new DateTime(2023, 12, 16), new DateTime(2023, 12, 16))).ToList();

            appointments[0].StartDate.Date.ShouldBeLessThan(new DateTime(2023, 12, 16));

        }

        [Fact]
        public void Available_appointments_when_date_is_priority()
        {
            #region Arrange

            ClearDbContext();
            var room = new Room {Name = "Ordination" };
            Context.Rooms.Add(room);
            Context.Shifts.Add(new Shift(3, "second", 13, 17));
            var firstDoctor = new Doctor(1, 3, new Specialization("General Practice", "")) { Room = room };
            var secondDoctor = new Doctor(2, 3, new Specialization("General Practice", "")) { Room = room };
            Context.Doctors.Add(firstDoctor);
            Context.Doctors.Add(secondDoctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 13, 0, 0),
                new DateTime(2023, 12, 16, 13, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 13, 30, 0),
                new DateTime(2023, 12, 16, 14, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 14, 0, 0),
                new DateTime(2023, 12, 16, 14, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 14, 30, 0),
                new DateTime(2023, 12, 16, 15, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 15, 0, 0),
                new DateTime(2023, 12, 16, 15, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 15, 30, 0),
                new DateTime(2023, 12, 16, 16, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 16, 0, 0),
                new DateTime(2023, 12, 16, 16, 30, 0), new DateTime(), 2, 1, firstDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, false, new DateTime(2023, 12, 16, 16, 30, 0),
                new DateTime(2023, 12, 16, 17, 0, 0), new DateTime(), 2, 1, firstDoctor));
            Context.SaveChanges();
            #endregion

            var appointments = _scheduleAppointmentService.GetAvailableAppointmentsForDatePriority(1, new TimePeriod(new DateTime(2023, 12, 16), new DateTime(2023, 12, 16))).ToList();

            appointments[0].Doctor.Id.ShouldBe(2);
        }

    }
}
