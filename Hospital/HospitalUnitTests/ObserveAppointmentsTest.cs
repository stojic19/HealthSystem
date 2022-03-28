using HospitalUnitTests.Base;
using Xunit;
using Hospital.Schedule.Model;
using System;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using Hospital.RoomsAndEquipment.Model;
using Shouldly;
using Hospital.Schedule.Service;
using System.Collections.Generic;

namespace HospitalUnitTests
{
    public class ObserveAppointmentsTest : BaseTest
    {
        private readonly ScheduledEventService _scheduledEventService;
        public ObserveAppointmentsTest(BaseFixture baseFixture) : base(baseFixture)
        {
            _scheduledEventService = new(UoW);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Observe_appointments(bool isCanceled, bool isDone, int finished, int canceled, int upcoming)
        {
            ClearDbContext();

            CreateDbContext(isCanceled, isDone);

            _scheduledEventService.GetFinishedUserEvents("testPatient").Count.ShouldBe(finished);
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(canceled);
            _scheduledEventService.GetUpcomingUserEvents("testPatient").Count.ShouldBe(upcoming);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>
            {
                new object[] { false, true, 1, 0, 0 },
                new object[] { true, false, 0, 1, 0 },
                new object[] { false, false, 0, 0, 1 }
            };

            return retVal;
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