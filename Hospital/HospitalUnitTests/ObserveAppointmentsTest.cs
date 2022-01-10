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
                StartDate = new DateTime(2021, 10, 17),
                EndDate = new DateTime(2021, 10, 17),
                PatientId = 10,
                DoctorId = 20,
                RoomId = 10
            }
            );
            Context.SaveChanges();
        }
        [Fact]
        public void Upcoming_event_becomes_finished()
        {
            ClearDbContext();

            createDbContext(isCanceled: false, isDone: false);

            ScheduledEventService scheduledEventsService = new(UoW);           
            scheduledEventsService.GetUpcomingUserEvents("testUser").Count.ShouldBe(1);
            scheduledEventsService.UpdateFinishedUserEvents();
            scheduledEventsService.GetFinishedUserEvents("testUser").Count.ShouldBe(1);

        }

        [Fact]
        public void Finished_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled:false,isDone:true);
           
            ScheduledEventService scheduledEventsService = new ScheduledEventService(UoW);
            scheduledEventsService.GetFinishedUserEvents("testUser").Count.ShouldBe(1);
            scheduledEventsService.GetCanceledUserEvents("testUser").Count.ShouldBe(0);
            scheduledEventsService.GetUpcomingUserEvents("testUser").Count.ShouldBe(0);
        }

        [Fact]
        public void Canceled_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: true, isDone: false);

            ScheduledEventService scheduledEventsService = new ScheduledEventService(UoW);
            scheduledEventsService.GetFinishedUserEvents("testUser").Count.ShouldBe(0);
            scheduledEventsService.GetCanceledUserEvents("testUser").Count.ShouldBe(1);
            scheduledEventsService.GetUpcomingUserEvents("testUser").Count.ShouldBe(0);
           
        }
    
        [Fact]
        public void Upcoming_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: false, isDone: false);

            ScheduledEventService scheduledEventsService = new ScheduledEventService(UoW);
            scheduledEventsService.GetFinishedUserEvents("testUser").Count.ShouldBe(0);
            scheduledEventsService.GetCanceledUserEvents("testUser").Count.ShouldBe(0);
            scheduledEventsService.GetUpcomingUserEvents("testUser").Count.ShouldBe(1);

        }

    }
}
