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
            scheduled.UpdateTime(scheduled.StartDate.AddDays(-3), scheduled.EndDate.AddDays(-3));
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

        private ScheduledEvent createDbContext(bool isCanceled, bool isDone)
        {
            Patient testPatient = new Patient();
            
            Context.Patients.Add(testPatient);
            Doctor testDoctor = new Doctor();
            Context.Doctors.Add(testDoctor);
            Context.Rooms.Add(new Room()
            {
                Id = 10
            });
            ScheduledEvent scheduledEvent = new ScheduledEvent(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                       new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);

            Context.ScheduledEvents.Add(scheduledEvent);

            Context.SaveChanges();
            return scheduledEvent;
        }
    }
}