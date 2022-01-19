using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using HospitalIntegrationTests.Base;
using System;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.Net;
using Hospital.SharedModel.Repository;
using Hospital.MedicalRecords.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalIntegrationTests
{
    public class ObserveAppointmentsTest : BaseTest
    {
        public ObserveAppointmentsTest(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Finished_events_count_should_not_be_zero()
        {
            RegisterAndLogin("Patient");
            var events = ArrangeDatabase(isCanceled: false, isDone: true);
            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/GetFinishedUserEvents/" + events.PatientId);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UoW.GetRepository<IScheduledEventReadRepository>().GetFinishedUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            ClearDatabase(events);
        }

        [Fact]
        public async Task Canceled_events_count_should_not_be_zero()
        {
            RegisterAndLogin("Patient");
            var events = ArrangeDatabase(isCanceled: true, isDone: false);
            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/GetCanceledUserEvents/" + events.Patient.Id);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            ClearDatabase(events);
        }
        [Fact]
        public async Task Upcoming_events_count_should_not_be_zero()
        {
            RegisterAndLogin("Patient");
            var events = ArrangeDatabase(isCanceled: false, isDone: false);
            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/GetUpcomingUserEvents/" + events.Patient.Id);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            UoW.GetRepository<IScheduledEventReadRepository>().GetUpcomingUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            ClearDatabase(events);
        }
        private void ClearDatabase(ScheduledEvent events)
        {
            UoW.GetRepository<IScheduledEventWriteRepository>().Delete(events,true);
            DeleteDataFromDataBase();

        }

        private ScheduledEvent ArrangeDatabase(bool isCanceled, bool isDone)
        {

            var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Include(d => d.Room).FirstOrDefault(x => x.UserName == "testDoctorUsername");
            var testPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.UserName == "testPatientUsername").FirstOrDefault();

            ScheduledEvent scheduledEvent = new ScheduledEvent(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);
            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
            return scheduledEvent;

        }
    }
}