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
        private readonly ScheduledEventService _scheduledEventService;
        public CancelingAppointmentTests(BaseFixture fixture) : base(fixture)
        {
            _scheduledEventService = new(UoW);
        }

        //TODO : KAKO HENDLOVATI DATUME
        //MOGLO BI SE RECI DA SU OVDJE ZBOG DATUMA TESTOVI CODEPENDANT 
        //IMA JEDAN FALSE POSITIVE ? OR IS IT
        [Fact]
        public void Appointment_should_be_cancelled()
        {
            ClearDbContext();
            ScheduledEvent events = CreateDbContext(isCanceled: false, isDone: false);
            Patient testPatient = events.Patient;

            testPatient.CancelAppointment(events.Id);
            testPatient.ScheduledEvents.Count.ShouldNotBe(0);
            testPatient.ScheduledEvents[0].IsCanceled.ShouldBeTrue();
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(1); //FALSE POSITIVE //novi act & assert
        }

        [Fact]
        public void Appointment_should_not_be_cancelled()
        {
            ClearDbContext();
            ScheduledEvent events  = CreateDbContext(isCanceled: false, isDone: false);
            Patient testPatient = events.Patient;

            UpdateEventTime(events);
            testPatient.CancelAppointment(events.Id);
            testPatient.ScheduledEvents[0].IsCanceled.ShouldBeFalse();
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(0);
        }

        private void UpdateEventTime(ScheduledEvent scheduled)
        {
            //TODO : DONE
            scheduled.UpdateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day));
            Context.SaveChanges();
        }

        [Fact]
        public void Finished_appointment_should_not_be_cancelled()
        {
            ClearDbContext();
            ScheduledEvent events = CreateDbContext(isCanceled: false, isDone: true);
            Patient testPatient = events.Patient;
            testPatient.CancelAppointment(events.Id);
            _scheduledEventService.GetCanceledUserEvents("testPatient").Count.ShouldBe(0);
        }

        private ScheduledEvent CreateDbContext(bool isCanceled, bool isDone)
        {
            Patient testPatient = new(1,"testPatient",new MedicalRecord());      
            Context.Patients.Add(testPatient);
 
            Doctor testDoctor = new(2, new Shift().Id, new Specialization(), new Room());          
            Context.Doctors.Add(testDoctor);
           
            ScheduledEvent scheduledEvent = new(0, isCanceled, isDone, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day),
                       new DateTime(), testPatient.Id, testDoctor.Id, testDoctor);

            Context.ScheduledEvents.Add(scheduledEvent);

            Context.SaveChanges();
            return scheduledEvent;
        }
    }
}