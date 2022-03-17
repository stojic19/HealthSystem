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
        private readonly ScheduledEventService _scheduledEventService;
        public ObserveAppointmentsTest(BaseFixture baseFixture) : base(baseFixture)
        {
            _scheduledEventService = new(UoW);
        }

        [Fact]
        public void Finished_events_count_should_not_be_zero()
        {
            ClearDbContext();

            CreateDbContext(isCanceled: false, isDone: true);

            _scheduledEventService.GetFinishedUserEvents("testPatient").Count.ShouldBe(1);
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(0);
            _scheduledEventService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(0);
        }

        [Fact]
        public void Canceled_events_count_should_not_be_zero()
        {
            ClearDbContext();

            CreateDbContext(isCanceled: true, isDone: false);

            _scheduledEventService.GetFinishedUserEvents("testPatient").Count.ShouldBe(0);
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(1);
            _scheduledEventService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(0);

        }

        [Fact]
        public void Upcoming_events_count_should_not_be_zero()
        {
            ClearDbContext();

            CreateDbContext(isCanceled: false, isDone: false);

            _scheduledEventService.GetFinishedUserEvents("testPatient").Count.ShouldBe(0);
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(0);
            _scheduledEventService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(1);

        }

        private void CreateDbContext(bool isCanceled, bool isDone)
        {

            Patient testPatient = new(1, "testPatient", new MedicalRecord());
            Context.Patients.Add(testPatient);

            Doctor testDoctor = new(2, new Shift().Id, new Specialization(), new Room());
            Context.Doctors.Add(testDoctor);

            ScheduledEvent scheduledEvent = new(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day),
                       new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);

            Context.ScheduledEvents.Add(scheduledEvent);
            Context.SaveChanges();
        }
    }
}