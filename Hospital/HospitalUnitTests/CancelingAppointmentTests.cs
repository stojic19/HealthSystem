using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using Xunit;

namespace HospitalUnitTests
{
    public class CancelingAppointmentTests : BaseTest
    {
        public CancelingAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }
        private void createDbContext(bool isCanceled, bool isDone)
        {
            Context.Patients.Add(new Patient()
            {
                Id = 10,
                UserName = "testUser"
            });
            Context.Doctors.Add(new Doctor()
            {
                Id = 20
            });
            Context.Rooms.Add(new Room()
            {
                Id = 10
            });

            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                ScheduledEventType = 0,
                IsCanceled = isCanceled,
                IsDone = isDone,
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                PatientId = 10,
                DoctorId = 20,
                RoomId = 10
            }
            );

            Context.SaveChanges();

        }
        [Fact]
        public void Appointment_should_be_cancelled()
        {
            ClearDbContext();
            createDbContext(isCanceled: false, isDone: false);
            ScheduledEventService scheduledEventsService = new(UoW);

            ScheduledEvent scheduled = scheduledEventsService.GetScheduledEvent(1);
            scheduledEventsService.CancelAppointment(1);
            scheduledEventsService.GetCanceledUserEvents("testUser").Count.ShouldNotBe(0);
        }
        [Fact]
        public void Appointment_should_not_be_cancelled()
        {
            ClearDbContext();
            createDbContext(isCanceled: false, isDone: false);
            ScheduledEventService scheduledEventsService = new(UoW);
            updateEventTime(scheduledEventsService);
            scheduledEventsService.CancelAppointment(1);
            scheduledEventsService.GetCanceledUserEvents("testUser").Count.ShouldBe(0);
        }

        private void updateEventTime(ScheduledEventService scheduledEventsService)
        {
            ScheduledEvent scheduled = scheduledEventsService.GetScheduledEvent(1);
            scheduled.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-2).Day);
            scheduled.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-2).Day);
            Context.SaveChanges();
        }

        [Fact]
        public void Finished_appointment_should_not_be_cancelled()
        {
            ClearDbContext();
            createDbContext(isCanceled: false, isDone: true);
            ScheduledEventService scheduledEventsService = new(UoW);
            scheduledEventsService.CancelAppointment(1);
            scheduledEventsService.GetCanceledUserEvents("testUser").Count.ShouldBe(0);
        }
    }
}