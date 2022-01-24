using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using HospitalIntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Repository;
using HospitalApi.DTOs;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace HospitalIntegrationTests
{
    public class BlockingMaliciousPatientsTests : BaseTest
    {
        public BlockingMaliciousPatientsTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Patient_should_be_blocked_request()
        {
            RegisterAndLogin("Manager");
            ArrangeDatabase();
            var patientRepo = UoW.GetRepository<IPatientReadRepository>();
            var patient = patientRepo.GetAll().FirstOrDefault(x => x.UserName == "testUsername");
            var userDTO = new UserForBlockingDTO
            {
                UserName = patient.UserName,
                LastName = patient.LastName,
                FirstName = patient.FirstName
            };
            var content = GetContent(userDTO);
            var response = await ManagerClient.PutAsync(BaseUrl + "api/BlockPatient/BlockPatient", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePatients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            foreach (var p in responsePatients.Where(p => p.UserName == "testUsername"))
            {
                p.IsBlocked.ShouldBe(true);
            }
        }

        [Fact]
        public async Task Patient_should_be_malicious_request()
        {
            RegisterAndLogin("Manager");
            ArrangeDataForGetMaliciousTrue();
            var patientRepo = UoW.GetRepository<IPatientReadRepository>();
            var isMalicious = false;
            var patient = patientRepo.GetAll().FirstOrDefault(x => x.UserName == "testUsername");
            var response = await ManagerClient.GetAsync(BaseUrl + "api/BlockPatient/GetMaliciousPatients");
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePatients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            foreach (var p in responsePatients.Where(p => p.UserName.Equals("testUsername2")))
            {
                isMalicious = true;
            }
            isMalicious.ShouldBe(true);
        }

        [Fact]
        public async Task Patient_should_not_be_malicious_request()
        {
            RegisterAndLogin("Manager");
            ArrangeDataForGetMaliciousFalse();
            var patientRepo = UoW.GetRepository<IPatientReadRepository>();
            var isMalicious = false;
            var patient = patientRepo.GetAll().FirstOrDefault(x => x.UserName == "testUsername");
            var response = await ManagerClient.GetAsync(BaseUrl + "api/BlockPatient/GetMaliciousPatients");
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePatients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            foreach (var p in responsePatients.Where(p => p.UserName.Equals("testUsername3")))
            {
                isMalicious = true;
            }
            isMalicious.ShouldBe(false);
        }

        private void ArrangeDataForGetMaliciousTrue()
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "TestRoom");

            if (room == null)
            {
                room = new Room()
                {
                    Name = "TestRoom",
                    Description = "Room for storage",
                    Width = 7,
                    Height = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testDoctorUsername");
            if (doctor == null)
            {
                doctor = new Doctor()
                {
                    FirstName = "TestDoctor",
                    LastName = "TestDoctorLastName",
                    MiddleName = "TestDoctorMiddleName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TestDoctorStreet",
                    Specialization = new Specialization("TestSpecialization", "DescriptionSpecialization"),
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Shift = new Shift()
                    {
                        From = 8,
                        To = 4,
                        Name = "prva"
                    },
                    Room = room,
                    City = new City("TestCity",00000,new Country("TestCountry")),
                    DoctorSchedule = new DoctorSchedule()

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername2");

            if (patient == null)
            {
                patient = new Patient(new MedicalRecord(null, 0, 0, doctor.Id, null))
                {
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    UserName = "testUsername2",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    IsBlocked = false

                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == doctor.Id);
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-110), patient.Id, doctor.Id, doctor));
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-27), patient.Id, doctor.Id, doctor));
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-14), patient.Id, doctor.Id, doctor));
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7), DateTime.Now.AddDays(-1), patient.Id, doctor.Id, doctor));
                UoW.GetRepository<IPatientWriteRepository>().Update(patient);

            }
        }
        private void ArrangeDataForGetMaliciousFalse()
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                   .GetAll()
                   .FirstOrDefault(x => x.Name == "TestRoom");

            if (room == null)
            {
                room = new Room()
                {
                    Name = "TestRoom",
                    Description = "Room for storage",
                    Width = 7,
                    Height = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testDoctorUsername");
            if (doctor == null)
            {
                doctor = new Doctor()
                {
                    FirstName = "TestDoctor",
                    LastName = "TestDoctorLastName",
                    MiddleName = "TestDoctorMiddleName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TestDoctorStreet",
                    Specialization = new Specialization("TestSpecialization", "DescriptionSpecialization"),
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Shift = new Shift()
                    {
                        From = 8,
                        To = 4,
                        Name = "prva"
                    },
                    Room = room,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    DoctorSchedule = new DoctorSchedule()
                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername3");

            if (patient == null)
            {
                patient = new Patient(new MedicalRecord(null, 0, 0, doctor.Id, null))
                {
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    UserName = "testUsername3",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    IsBlocked = false

                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == doctor.Id);
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-110), patient.Id, doctor.Id, doctor));
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-27), patient.Id, doctor.Id, doctor));
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, false, true, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-4), new DateTime(), patient.Id, doctor.Id, doctor));
                patient.ScheduledEvents.Add(new ScheduledEvent(ScheduledEventType.Appointment, true, false, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7), DateTime.Now.AddDays(-1), patient.Id, doctor.Id, doctor));
                UoW.GetRepository<IPatientWriteRepository>().Update(patient);
            }
        }


        private void ArrangeDatabase()
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "TestRoom");

            if (room == null)
            {
                room = new Room()
                {
                    Name = "TestRoom",
                    Description = "Room for storage",
                    Width = 7,
                    Height = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testDoctorUsername");
            if (doctor == null)
            {
                doctor = new Doctor()
                {
                    FirstName = "TestDoctor",
                    LastName = "TestDoctorLastName",
                    MiddleName = "TestDoctorMiddleName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TestDoctorStreet",
                    Specialization = new Specialization("TestSpecialization", "DescriptionSpecialization"),
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Shift = new Shift()
                    {
                        From = 8,
                        To = 4,
                        Name = "prva"
                    },
                    Room = room,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    DoctorSchedule = new DoctorSchedule()

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername");

            if (patient == null)
            {
                patient = new Patient(new MedicalRecord(null,0,0,doctor.Id,null))
                {
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    UserName = "testUsername",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    City = new City("TestCity", 00000, new Country("TestCountry")),
                    IsBlocked = false
                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

            }


        }

        private void ClearDatabase()
        {
            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
               .FirstOrDefault(x => x.UserName == "testUsername");
            if (patient == null) return;

            {
                var medicalRecord = UoW.GetRepository<IMedicalRecordReadRepository>()
                    .GetAll().Include(mr => mr.Doctor)
                    .FirstOrDefault(x => x.Id == patient.MedicalRecordId);

                UoW.GetRepository<IMedicalRecordWriteRepository>().Delete(medicalRecord);
            }
            var doctor = UoW.GetRepository<IDoctorReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testDoctorUsername");
            if (doctor != null)
            {
                UoW.GetRepository<IDoctorWriteRepository>().Delete(doctor);
            }
            //var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
            //    .FirstOrDefault(x => x.Name == "TestSpecialization");
            //if (specialization != null)
            //{
            //    UoW.GetRepository<ISpecializationWriteRepository>().Delete(specialization);
            //}
            //var city = UoW.GetRepository<ICityReadRepository>()
            //    .GetAll().ToList()
            //    .FirstOrDefault(x => x.Name == "TestCity");

            //if (city != null)
            //{
            //    UoW.GetRepository<ICityWriteRepository>().Delete(city);
            //}

            //var country = UoW.GetRepository<ICountryReadRepository>()
            //    .GetAll().ToList()
            //    .FirstOrDefault(x => x.Name == "TestCountry");

            //if (country != null)
            //{
            //    UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            //}

            var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll().ToList()
                    .FirstOrDefault(x => x.Name == "TestRoom");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }
            var shift = UoW.GetRepository<IShiftReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "prva");

            if (shift != null)
            {
                UoW.GetRepository<IShiftWriteRepository>().Delete(shift);
            }
        }
    }
}
