using HospitalUnitTests.Base;
using Xunit;
using Hospital.Schedule.Model;
using System;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.RoomsAndEquipment.Model;
using Shouldly;
using Hospital.Schedule.Service;

namespace HospitalUnitTests
{
    public class ObserveAppointmentsTest : BaseTest
    {
        public ObserveAppointmentsTest(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Finished_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: false, isDone: true);

            ScheduledEventService scheduledEventsService = new ScheduledEventService(UoW);
            scheduledEventsService.GetFinishedUserEvents("testPatient").Count.ShouldBe(1);
            scheduledEventsService.GetCanceledUserEvents("testPatient").Count.ShouldBe(0);
            scheduledEventsService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(0);
        }

        [Fact]
        public void Canceled_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: true, isDone: false);

            ScheduledEventService scheduledEventsService = new ScheduledEventService(UoW);
            scheduledEventsService.GetFinishedUserEvents("testPatient").Count.ShouldBe(0);
            scheduledEventsService.GetCanceledUserEvents("testPatient").Count.ShouldBe(1);
            scheduledEventsService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(0);

        }

        [Fact]
        public void Upcoming_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: false, isDone: false);

            ScheduledEventService scheduledEventsService = new ScheduledEventService(UoW);
            scheduledEventsService.GetFinishedUserEvents("testPatient").Count.ShouldBe(0);
            scheduledEventsService.GetCanceledUserEvents("testPatient").Count.ShouldBe(0);
            scheduledEventsService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(1);

        }


        private void createDbContext(bool isCanceled, bool isDone)
        {

            Patient testPatient = new Patient(1, "testPatient", new MedicalRecord());
            Context.Patients.Add(testPatient);

            Doctor testDoctor = new Doctor(2, new Shift().Id, new Specialization(), new Room());
            Context.Doctors.Add(testDoctor);

            ScheduledEvent scheduledEvent = new ScheduledEvent(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day),
                       new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);

            Context.ScheduledEvents.Add(scheduledEvent);
            Context.SaveChanges();
        }

    }
}