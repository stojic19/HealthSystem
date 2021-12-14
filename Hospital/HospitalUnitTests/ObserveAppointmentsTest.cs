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
                Id = 10
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

            ScheduledEventsService scheduledEventsService = new(UoW);           
            scheduledEventsService.getUpcomingUserEvents(10).Count.ShouldBe(1);
            scheduledEventsService.updateFinishedUserEvents();
            scheduledEventsService.getFinishedUserEvents(10).Count.ShouldBe(1);

        }

        [Fact]
        public void Finished_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled:false,isDone:true);
           
            ScheduledEventsService scheduledEventsService = new ScheduledEventsService(UoW);
            scheduledEventsService.getFinishedUserEvents(10).Count.ShouldBe(1);
            scheduledEventsService.getCanceledUserEvents(10).Count.ShouldBe(0);
            scheduledEventsService.getUpcomingUserEvents(10).Count.ShouldBe(0);
        }

        [Fact]
        public void Canceled_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: true, isDone: false);

            ScheduledEventsService scheduledEventsService = new ScheduledEventsService(UoW);
            scheduledEventsService.getFinishedUserEvents(10).Count.ShouldBe(0);
            scheduledEventsService.getCanceledUserEvents(10).Count.ShouldBe(1);
            scheduledEventsService.getUpcomingUserEvents(10).Count.ShouldBe(0);
           
        }
    
        [Fact]
        public void Upcoming_events_count_should_not_be_zero()
        {
            ClearDbContext();

            createDbContext(isCanceled: false, isDone: false);

            ScheduledEventsService scheduledEventsService = new ScheduledEventsService(UoW);
            scheduledEventsService.getFinishedUserEvents(10).Count.ShouldBe(0);
            scheduledEventsService.getCanceledUserEvents(10).Count.ShouldBe(0);
            scheduledEventsService.getUpcomingUserEvents(10).Count.ShouldBe(1);

        }

    }
}
