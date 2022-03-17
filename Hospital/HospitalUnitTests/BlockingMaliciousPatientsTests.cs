using System;
using System.Linq;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using HospitalUnitTests.Base;
using Shouldly;
using Xunit;

namespace HospitalUnitTests
{
    public class BlockingMaliciousPatientsTests : BaseTest
    {
        private readonly BlockingService _blockingService;

        public BlockingMaliciousPatientsTests(BaseFixture fixture) : base(fixture)
        {
            _blockingService = new BlockingService(UoW);
        }

        [Fact]
        public void Patient_should_be_malicious()
        {
            #region Arange

            ClearDbContext();
            //Context.Rooms.Add(new Room { Id = 1, Name = "Ordination" });
            (Doctor testDoctor, Patient testPatient) = ArrangeData();
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-100),
                 DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-110), testPatient.Id, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-18),
                DateTime.Now.AddDays(-18), new DateTime(), testPatient.Id, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-22),
                 DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-27), testPatient.Id, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-4),
                 DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-14), testPatient.Id, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(7),
                 DateTime.Now.AddDays(7), DateTime.Now, testPatient.Id, 1, testDoctor));

            Context.SaveChanges();
            #endregion

            //var patient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            //if (patient == null) return;    //TODO: Da hell je ovaj uslov ?
            var maliciousPatients = _blockingService.GetMaliciousPatients();
            maliciousPatients.ShouldNotBe(null);
            maliciousPatients.ShouldContain(testPatient);
        }

        [Fact]
        public void Patient_should_not_be_malicious()
        {
            #region Arange

            ClearDbContext();
            (Doctor testDoctor, Patient testPatient) = ArrangeData();

            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-100),
                 DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-110), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-22),
                DateTime.Now.AddDays(-22), new DateTime(), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-19),
                DateTime.Now.AddDays(-19), new DateTime(), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-4),
                 DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-14), 2, 1, testDoctor));

            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(7),
                 DateTime.Now.AddDays(7), DateTime.Now.AddDays(-1), 2, 1, testDoctor));

            Context.SaveChanges();
            #endregion
       
            //var patient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            //if (patient == null) return;
            var maliciousPatients = _blockingService.GetMaliciousPatients();
            maliciousPatients.ShouldNotBe(null);
            maliciousPatients.ShouldNotContain(testPatient);
        }

        [Fact]
        public void Patient_should_be_malicious_and_blocked()
        {
            #region Arrange

            ClearDbContext();
            (Doctor testDoctor, Patient testPatient) = ArrangeData();

            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-100),
                 DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-110), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-18),
                DateTime.Now.AddDays(-18), new DateTime(), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-22),
                 DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-27), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-19),
                DateTime.Now.AddDays(-19), new DateTime(), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-4),
                 DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-14), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(7),
                 DateTime.Now.AddDays(7), DateTime.Now, 2, 1, testDoctor));

            Context.SaveChanges();

            #endregion

            testPatient.Block();
            var maliciousPatients = _blockingService.GetMaliciousPatients();
            maliciousPatients.ShouldContain(testPatient);
            testPatient.IsBlocked.ShouldBe(true);

        }
        [Fact]
        /**
         * //NE TREBA DA RADI STA POSLOVNA LOGIKA RADI 
            //Leaking algorithm Implementation
            //Aim at the end result instead of implementation details = Resistence to refactoring
         */
        public void Patient_should_not_be_blocked()
        {
            #region Arange

            ClearDbContext();
            (Doctor testDoctor, Patient testPatient) = ArrangeData();

            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-100),
                 DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-110), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-22),
                DateTime.Now.AddDays(-22), new DateTime(), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-19),
                DateTime.Now.AddDays(-19), new DateTime(), 2, 1, testDoctor));
            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-4),
                 DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-14), 2, 1, testDoctor));

            Context.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(7),
                 DateTime.Now.AddDays(7), DateTime.Now.AddDays(-1), 2, 1, testDoctor));

            Context.SaveChanges();
            #endregion
      
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(3);
            if (patient == null) return;
            var maliciousPatients = _blockingService.GetMaliciousPatients();
            
            foreach (var malicious in maliciousPatients.Where(malicious => malicious.Id == patient.Id))
            {
                _blockingService.BlockPatient(patient.UserName);
            }
            var blockedPatient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            blockedPatient.IsBlocked.ShouldBe(false);
        }

        [Fact]
        public void Patient_should_be_blocked()
        {
            #region Arange

            ClearDbContext();
            Context.Rooms.Add(new Room { Id = 1, Name = "Ordination" });
            Context.Shifts.Add(new Shift(3, "first", 7, 15));
            var room = new Room()
            {
                Id = 2,
                BuildingName = "hf",
                Description = "dada",
                FloorNumber = 2,
                Name = "duno"
            };
            Context.Rooms.Add(room);
            var firstDoctor = new Doctor(1, 3, new Specialization("General Practice", ""), room);
            Context.Doctors.Add(firstDoctor);
            var medicalRecord = new MedicalRecord(1, null, 0, 0, 1, null);
            Context.MedicalRecords.Add(medicalRecord);
            var newPatient = new Patient(2, "testPatient", medicalRecord);
            Context.Patients.Add(newPatient);

            Context.SaveChanges();

            #endregion
           
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(newPatient.Id);
            _blockingService.BlockPatient(patient.UserName);
            var blockedPatient = UoW.GetRepository<IPatientReadRepository>().GetById(newPatient.Id);
            blockedPatient.IsBlocked.ShouldBe(true);
        }
        private (Doctor doctor, Patient patient) ArrangeData()
        {
            Context.Shifts.Add(new Shift(3, "first", 7, 15));
            var room = new Room()
            {
                Id = 2,
                BuildingName = "TestHospital",
                Description = "TestHospital",
                FloorNumber = 2,
                Name = "TestRoom"
            };
            Context.Rooms.Add(room);
            var testDoctor = new Doctor(1, 3, new Specialization("General Practice", ""), room);
            Context.Doctors.Add(testDoctor);

            var testPatient = new Patient(2, "testPatient", new MedicalRecord(1, null, 0, 0, 1, null));
            Context.Patients.Add(testPatient);
            return (testDoctor, testPatient);
        }
    }
}
