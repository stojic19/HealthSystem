using System;
using System.Linq;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
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
            var shift = new Shift()
            {
                Name = "test shift",
                From = 7,
                To = 15
            };
            Context.Shifts.Add(shift);
            var room = new Room()
            {
                Name = "test rooom"
            };
            Context.Rooms.Add(room);
            var doctor = new Doctor()
            {
                RoomId = room.Id,
                ShiftId = shift.Id,
                Specialization = new Specialization("General Practice", "")
            };
            Context.Doctors.Add(doctor);
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 00, 00),
                new DateTime(2022, 12, 10, 7, 30, 00), new DateTime(), 1, doctor.Id, doctor));
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
            var shift = new Shift()
            {
                Name = "test shift",
                From = 7,
                To = 15
            };
            Context.Shifts.Add(shift);
            var room = new Room()
            {
                Name = "test rooom"
            };
            Context.Rooms.Add(room);
            var dr = new Doctor()
            {
                RoomId = room.Id,
                ShiftId = shift.Id,
                Specialization = new Specialization("General Practice", "")
            };
            Context.Doctors.Add(dr);
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 00, 00),
                new DateTime(2022, 12, 10, 7, 30, 00), new DateTime(), 1, dr.Id, dr));
            Context.SaveChanges();
            
            var preferredDate = new DateTime(2022, 12, 10, 7, 00, 00);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));

            var repo = UoW.GetRepository<IDoctorReadRepository>();
            var isDoctorAvailable = repo.IsDoctorAvailableInTerm(doctor.Id, preferredDate);

            isDoctorAvailable.ShouldBeFalse();
        }

        [Fact]
        public void Should_return_available_appointments()
        {
            ClearDbContext();
            var shift = new Shift()
            {
                Name = "test shift",
                From = 7,
                To = 15
            };
            Context.Shifts.Add(shift);
            var room = new Room()
            {
                Name = "test rooom"
            };
            Context.Rooms.Add(room);
            var dr = new Doctor()
            {
                RoomId = room.Id,
                ShiftId = shift.Id,
                Specialization = new Specialization("General Practice", "")
            };
            Context.Doctors.Add(dr);
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 9, 13, 00, 00),
                new DateTime(2022, 12, 9, 13, 30, 00), new DateTime(), 1, dr.Id, dr));
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 15, 00, 00),
                new DateTime(2022, 12, 10, 15, 30, 00), new DateTime(), 1, dr.Id, dr));
            Context.SaveChanges();

            var preferredDate = new DateTime(2021, 12, 9);

            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Specialization)
                .FirstOrDefault(s => s.Specialization.Name.ToLower().Equals("general practice"));
            
            var service = new ScheduleAppointmentService(UoW);
            var availableTerms = service.GetAvailableTermsForDoctorAndDate(doctor.Id, preferredDate).ToList();

            availableTerms.ShouldNotBeNull();
            availableTerms.Count.ShouldBe(16);
        }


        [Fact]
        public void Should_return_empty_available_appointments()
        {
            ClearDbContext();
            var shift = new Shift()
            {
                Name = "test shift",
                From = 7,
                To = 15
            };
            Context.Shifts.Add(shift);
            var room = new Room()
            {
                Name = "test rooom"
            };
            Context.Rooms.Add(room);
            var dr = new Doctor()
            {
                RoomId = room.Id,
                ShiftId = shift.Id,
                Specialization = new Specialization("General Practice", "")
            };
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
                new DateTime(2022, 12, 10, 7, 30, 00), new DateTime(), 1, dr.Id, dr));
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 7, 30, 00),
                new DateTime(2022, 12, 10, 8, 00, 00), new DateTime(), 1, dr.Id, dr));
            Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 8, 00, 00),
                 new DateTime(2022, 12, 10, 8, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 8, 30, 00),
                 new DateTime(2022, 12, 10, 9, 00, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 9, 00, 00),
                 new DateTime(2022, 12, 10, 9, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 9, 30, 00),
                 new DateTime(2022, 12, 10, 10, 00, 00),  new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 10, 00, 00),
                 new DateTime(2022, 12, 10, 10, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 10, 30, 00),
                 new DateTime(2022, 12, 10, 11, 00, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 11, 00, 00),
                 new DateTime(2022, 12, 10, 11, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 11, 30, 00),
                 new DateTime(2022, 12, 10, 12, 00, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 12, 00, 00),
                 new DateTime(2022, 12, 10, 12, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 12, 30, 00),
                 new DateTime(2022, 12, 10, 13, 00, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 13, 00, 00),
                 new DateTime(2022, 12, 10, 13, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 13, 30, 00),
                 new DateTime(2022, 12, 10, 14, 00, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 14, 00, 00),
                 new DateTime(2022, 12, 10, 14, 30, 00), new DateTime(), 1, dr.Id, dr));
             Context.ScheduledEvents.Add(new ScheduledEvent(0, false, false, new DateTime(2022, 12, 10, 14, 30, 00),
                 new DateTime(2022, 12, 10, 15, 00, 00), new DateTime(), 1, dr.Id, dr));
        }
    }
}
