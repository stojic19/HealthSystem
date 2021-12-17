using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public BlockingMaliciousPatientsTests(BaseFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public void Patient_should_be_malicious()
        {
            #region
            ClearDbContext();
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "BiH"
            });
            Context.Cities.Add(new City()
            {
                Id = 1,
                Name = "Bijeljina",
                CountryId = 1
            });
            Context.Specializations.Add(new Specialization()
            {
                Id = 1,
                Name = "GP",
                Description = "Doktor opste prakse"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                BuildingName = "hf",
                Description = "dada",
                FloorNumber = 2,
                Name = "duno"
            });

            Context.Users.Add(new Doctor()
            {
                Id = 1,
                UserName = "username1",
                Email = "mail1",
                Gender = Gender.Male,
                FirstName = "name",
                LastName = "lastname",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-35),
                MiddleName = "middleName",
                SpecializationId = 1,
                EmailConfirmed = true,
                Street = "street1",
                StreetNumber = "1",
                RoomId = 1,
                PhoneNumber = "213122122",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 1,
                Height = 172,
                Weight = 72,
                DoctorId = 1,
                BloodType = BloodType.ABNegative,
                JobStatus = JobStatus.Student
            });
            Context.Users.Add(new Patient()
            {
                Id = 2,
                UserName = "testPatient",
                Email = "mailPatient",
                Gender = Gender.Female,
                FirstName = "name1",
                LastName = "lastname1",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-25),
                MiddleName = "middle",
                EmailConfirmed = true,
                Street = "street2",
                StreetNumber = "3",
                PhoneNumber = "213124422",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                MedicalRecordId = 1,
                IsBlocked = false
            });

            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-100),
                EndDate = DateTime.Now.AddDays(-100),
                CancellationDate = DateTime.Now.AddDays(-110),
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-18),
                EndDate = DateTime.Now.AddDays(-18),
                PatientId = 2,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-22),
                EndDate = DateTime.Now.AddDays(-22),
                CancellationDate = DateTime.Now.AddDays(-27),
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-19),
                EndDate = DateTime.Now.AddDays(-19),
                PatientId = 2,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now.AddDays(-4),
                CancellationDate = DateTime.Now.AddDays(-14),
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7),
                CancellationDate = DateTime.Now,
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.SaveChanges();

            #endregion

            var blockingService = new BlockingService(UoW);
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            if (patient == null) return;
            var maliciousPatients = blockingService.GetMaliciousPatients();
            maliciousPatients.ShouldNotBe(null);
            maliciousPatients.ShouldContain(patient);


        }
        [Fact]
        public void Patient_should_not_be_malicious()
        {
            #region
            ClearDbContext();
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "BiH"
            });
            Context.Cities.Add(new City()
            {
                Id = 1,
                Name = "Bijeljina",
                CountryId = 1
            });
            Context.Specializations.Add(new Specialization()
            {
                Id = 1,
                Name = "GP",
                Description = "Doktor opste prakse"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                BuildingName = "hf",
                Description = "dada",
                FloorNumber = 2,
                Name = "duno"
            });

            Context.Users.Add(new Doctor()
            {
                Id = 1,
                UserName = "username1",
                Email = "mail1",
                Gender = Gender.Male,
                FirstName = "name",
                LastName = "lastname",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-35),
                MiddleName = "middleName",
                SpecializationId = 1,
                EmailConfirmed = true,
                Street = "street1",
                StreetNumber = "1",
                RoomId = 1,
                PhoneNumber = "213122122",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });


            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 1,
                Height = 172,
                Weight = 72,
                DoctorId = 1,
                BloodType = BloodType.ABNegative,
                JobStatus = JobStatus.Student
            });
            Context.Users.Add(new Patient()
            {
                Id = 3,
                UserName = "testPatient",
                Email = "mailPatient",
                Gender = Gender.Female,
                FirstName = "name1",
                LastName = "lastname1",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-25),
                MiddleName = "middle",
                EmailConfirmed = true,
                Street = "street2",
                StreetNumber = "3",
                PhoneNumber = "213124422",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                MedicalRecordId = 1
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-100),
                EndDate = DateTime.Now.AddDays(-100),
                CancellationDate = DateTime.Now.AddDays(-110),
                PatientId = 3,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-22),
                EndDate = DateTime.Now.AddDays(-22),
                PatientId = 3,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-19),
                EndDate = DateTime.Now.AddDays(-19),
                PatientId = 3,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now.AddDays(-4),
                CancellationDate = DateTime.Now.AddDays(-14),
                PatientId = 3,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7),
                PatientId = 3,
                CancellationDate = DateTime.Now.AddDays(-1),
                IsDone = false,
                IsCanceled = true

            });


            Context.SaveChanges();
            #endregion

            var blockingService = new BlockingService(UoW);
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(3);
            if (patient == null) return;
            var maliciousPatients = blockingService.GetMaliciousPatients();
            maliciousPatients.ShouldNotBe(null);
            maliciousPatients.ShouldNotContain(patient);
        }

        [Fact]
        public void Patient_should_be_malicious_and_blocked()
        {
            #region
            ClearDbContext();
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "BiH"
            });
            Context.Cities.Add(new City()
            {
                Id = 1,
                Name = "Bijeljina",
                CountryId = 1
            });
            Context.Specializations.Add(new Specialization()
            {
                Id = 1,
                Name = "GP",
                Description = "Doktor opste prakse"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                BuildingName = "hf",
                Description = "dada",
                FloorNumber = 2,
                Name = "duno"
            });

            Context.Users.Add(new Doctor()
            {
                Id = 1,
                UserName = "username1",
                Email = "mail1",
                Gender = Gender.Male,
                FirstName = "name",
                LastName = "lastname",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-35),
                MiddleName = "middleName",
                SpecializationId = 1,
                EmailConfirmed = true,
                Street = "street1",
                StreetNumber = "1",
                RoomId = 1,
                PhoneNumber = "213122122",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 1,
                Height = 172,
                Weight = 72,
                DoctorId = 1,
                BloodType = BloodType.ABNegative,
                JobStatus = JobStatus.Student
            });
            Context.Users.Add(new Patient()
            {
                Id = 2,
                UserName = "testPatient",
                Email = "mailPatient",
                Gender = Gender.Female,
                FirstName = "name1",
                LastName = "lastname1",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-25),
                MiddleName = "middle",
                EmailConfirmed = true,
                Street = "street2",
                StreetNumber = "3",
                PhoneNumber = "213124422",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                MedicalRecordId = 1,
                IsBlocked = false
            });

            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-100),
                EndDate = DateTime.Now.AddDays(-100),
                CancellationDate = DateTime.Now.AddDays(-110),
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-18),
                EndDate = DateTime.Now.AddDays(-18),
                PatientId = 2,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-22),
                EndDate = DateTime.Now.AddDays(-22),
                CancellationDate = DateTime.Now.AddDays(-27),
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-19),
                EndDate = DateTime.Now.AddDays(-19),
                PatientId = 2,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now.AddDays(-4),
                CancellationDate = DateTime.Now.AddDays(-14),
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7),
                CancellationDate = DateTime.Now,
                PatientId = 2,
                IsDone = false,
                IsCanceled = true

            });
            Context.SaveChanges();

            #endregion

            var blockingService = new BlockingService(UoW);
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            if (patient == null) return;
            var maliciousPatients = blockingService.GetMaliciousPatients();
            foreach (var malicious in maliciousPatients.Where(malicious => malicious.Id == patient.Id))
            {
                blockingService.BlockPatient(patient.UserName);
            }
            var blockedPatient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            blockedPatient.IsBlocked.ShouldBe(true);

        }
        [Fact]
        public void Patient_should_not_be_blocked()
        {
            #region
            ClearDbContext();
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "BiH"
            });
            Context.Cities.Add(new City()
            {
                Id = 1,
                Name = "Bijeljina",
                CountryId = 1
            });
            Context.Specializations.Add(new Specialization()
            {
                Id = 1,
                Name = "GP",
                Description = "Doktor opste prakse"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                BuildingName = "hf",
                Description = "dada",
                FloorNumber = 2,
                Name = "duno"
            });

            Context.Users.Add(new Doctor()
            {
                Id = 1,
                UserName = "username1",
                Email = "mail1",
                Gender = Gender.Male,
                FirstName = "name",
                LastName = "lastname",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-35),
                MiddleName = "middleName",
                SpecializationId = 1,
                EmailConfirmed = true,
                Street = "street1",
                StreetNumber = "1",
                RoomId = 1,
                PhoneNumber = "213122122",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });


            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 1,
                Height = 172,
                Weight = 72,
                DoctorId = 1,
                BloodType = BloodType.ABNegative,
                JobStatus = JobStatus.Student
            });
            Context.Users.Add(new Patient()
            {
                Id = 3,
                UserName = "testPatient",
                Email = "mailPatient",
                Gender = Gender.Female,
                FirstName = "name1",
                LastName = "lastname1",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-25),
                MiddleName = "middle",
                EmailConfirmed = true,
                Street = "street2",
                StreetNumber = "3",
                PhoneNumber = "213124422",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                MedicalRecordId = 1
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-100),
                EndDate = DateTime.Now.AddDays(-100),
                CancellationDate = DateTime.Now.AddDays(-110),
                PatientId = 3,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-22),
                EndDate = DateTime.Now.AddDays(-22),
                PatientId = 3,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-19),
                EndDate = DateTime.Now.AddDays(-19),
                PatientId = 3,
                IsDone = true,
                IsCanceled = false

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now.AddDays(-4),
                CancellationDate = DateTime.Now.AddDays(-14),
                PatientId = 3,
                IsDone = false,
                IsCanceled = true

            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7),
                PatientId = 3,
                CancellationDate = DateTime.Now.AddDays(-1),
                IsDone = false,
                IsCanceled = true

            });


            Context.SaveChanges();
            #endregion

            var blockingService = new BlockingService(UoW);
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(3);
            if (patient == null) return;
            var maliciousPatients = blockingService.GetMaliciousPatients();
            foreach (var malicious in maliciousPatients.Where(malicious => malicious.Id == patient.Id))
            {
                blockingService.BlockPatient(patient.UserName);
            }
            var blockedPatient = UoW.GetRepository<IPatientReadRepository>().GetById(3);
            blockedPatient.IsBlocked.ShouldBe(false);
        }

        [Fact]
        public void Patient_should_be_blocked()
        {
            #region
            ClearDbContext();
            Context.Countries.Add(new Country()
            {
                Id = 1,
                Name = "BiH"
            });
            Context.Cities.Add(new City()
            {
                Id = 1,
                Name = "Bijeljina",
                CountryId = 1
            });
            Context.Specializations.Add(new Specialization()
            {
                Id = 1,
                Name = "GP",
                Description = "Doktor opste prakse"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                BuildingName = "hf",
                Description = "dada",
                FloorNumber = 2,
                Name = "duno"
            });

            Context.Users.Add(new Doctor()
            {
                Id = 1,
                UserName = "username1",
                Email = "mail1",
                Gender = Gender.Male,
                FirstName = "name",
                LastName = "lastname",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-35),
                MiddleName = "middleName",
                SpecializationId = 1,
                EmailConfirmed = true,
                Street = "street1",
                StreetNumber = "1",
                RoomId = 1,
                PhoneNumber = "213122122",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            Context.MedicalRecords.Add(new MedicalRecord()
            {
                Id = 1,
                Height = 172,
                Weight = 72,
                DoctorId = 1,
                BloodType = BloodType.ABNegative,
                JobStatus = JobStatus.Student
            });
            Context.Users.Add(new Patient()
            {
                Id = 2,
                UserName = "testPatient",
                Email = "mailPatient",
                Gender = Gender.Female,
                FirstName = "name1",
                LastName = "lastname1",
                CityId = 1,
                DateOfBirth = DateTime.Now.AddYears(-25),
                MiddleName = "middle",
                EmailConfirmed = true,
                Street = "street2",
                StreetNumber = "3",
                PhoneNumber = "213124422",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                MedicalRecordId = 1,
                IsBlocked = false
            });

          
            Context.SaveChanges();

            #endregion

            var blockingService = new BlockingService(UoW);
            var patient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            blockingService.BlockPatient(patient.UserName);
            var blockedPatient = UoW.GetRepository<IPatientReadRepository>().GetById(2);
            blockedPatient.IsBlocked.ShouldBe(true);

        }

    }
}
