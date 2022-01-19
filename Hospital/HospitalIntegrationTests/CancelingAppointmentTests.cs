using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository;
using HospitalIntegrationTests.Base;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class CancelingAppointmentTests : BaseTest
    {
        public CancelingAppointmentTests(BaseFixture fixture) : base(fixture)
        {
        }
       
        [Fact]
        public async Task Appointment_should_be_canceled()
        {
            RegisterAndLogin("Patient");
            var events = AddTestDataToDatabase(isCanceled: false, isDone: false);
           
            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/CancelAppointment?eventId=" + events.Id + "&username=" + "testPatientUsername");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            DeleteTestDataFromDataBase(events);
            DeleteDataFromDataBase();

        }

        [Fact]
        public async Task Finished_appointment_should_not_be_canceled()
        {
            RegisterAndLogin("Patient");
            var events = AddTestDataToDatabase(isCanceled: false, isDone: true);
           

            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/CancelAppointment?eventId=" + events.Id + "&username=" + "testPatientUsername");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(events.Patient.UserName).Count.ShouldBe(0);
            DeleteTestDataFromDataBase(events);
            DeleteDataFromDataBase();
        }

        [Fact]
        public async Task Appointment_should_not_be_canceled()
        {
            RegisterAndLogin("Patient");
            var events = AddTestDataToDatabase(isCanceled: false, isDone: false);
            UpdateEventTime(events);
            

            var response = await PatientClient.GetAsync(BaseUrl + "api/ScheduledEvent/CancelAppointment?eventId=" + events.Id + "&username=" + "testPatientUsername");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(events.Patient.UserName).Count.ShouldBe(0);
            DeleteTestDataFromDataBase(events);
            DeleteDataFromDataBase();
        }

        private void UpdateEventTime(ScheduledEvent events)
        {
            events.UpdateTime(events.StartDate.AddDays(-3), events.EndDate.AddDays(-3));
            UoW.GetRepository<IScheduledEventWriteRepository>().Update(events, true);
        }

        private void DeleteTestDataFromDataBase(ScheduledEvent events)
        {
            UoW.GetRepository<IScheduledEventWriteRepository>().Delete(events, true);
            
        }
        private ScheduledEvent AddTestDataToDatabase(bool isCanceled, bool isDone)
        {
            var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().FirstOrDefault(x => x.FirstName == "TestDoctor");
            var testPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().FirstOrDefault(x => x.FirstName == "TestPatient");
            var testRoom = UoW.GetRepository<IRoomReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestRoom");

            testPatient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testPatientUsername");
            ScheduledEvent scheduledEvent = new ScheduledEvent(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                          new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
            return scheduledEvent;
        }

    }
}
