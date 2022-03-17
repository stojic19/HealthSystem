using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace HospitalUnitTests
{
    public class ObserveReportTests : BaseTest
    {
        public ObserveReportTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Report_count_should_not_be_zero()
        {
            ClearDbContext();
            var eventId = CreateDbContext();
            ReportService service = new(UoW);
            service.GetReport(eventId).ShouldNotBe(null);
            service.GetAllReports().ToList().Count.ShouldNotBe(0);
        }

        private int CreateDbContext()
        {
            Patient testPatient = new(1, "testPatient", new MedicalRecord());
            Context.Patients.Add(testPatient);

            Doctor testDoctor = new(2, new Shift().Id, new Specialization(), new Room());
            Context.Doctors.Add(testDoctor);

            ScheduledEvent scheduledEvent = new(0, false, false, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day),
                       new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);

            Context.ScheduledEvents.Add(scheduledEvent);
            scheduledEvent.SetToDone();
            Context.SaveChanges();
            return scheduledEvent.Id;
        }
    }

   
}

