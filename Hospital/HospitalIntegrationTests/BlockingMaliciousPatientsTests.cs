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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

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
            var response = await Client.PutAsync(BaseUrl + "api/BlockPatient/BlockPatient", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePatients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            foreach (var p in responsePatients.Where(p => p.UserName == "testUsername"))
            {
                p.IsBlocked.ShouldBe(true);
            }
            ClearDatabase();
        }

        [Fact]
        public async Task Patient_should_be_malicious_request()
        {
            ArrangeDataForGetMaliciousTrue();
            var patientRepo = UoW.GetRepository<IPatientReadRepository>();
            var isMalicious = false;
            var patient = patientRepo.GetAll().FirstOrDefault(x => x.UserName == "testUsername");
            var response = await Client.GetAsync(BaseUrl + "api/BlockPatient/GetMaliciousPatients");
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePatients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            foreach (var p in responsePatients.Where(p => p.UserName.Equals("testUsername")))
            {
                isMalicious = true;
            }
            isMalicious.ShouldBe(true);
            ClearDatabase();
        }

        [Fact]
        public async Task Patient_should_not_be_malicious_request()
        {
            ArrangeDataForGetMaliciousFalse();
            var patientRepo = UoW.GetRepository<IPatientReadRepository>();
            var isMalicious = false;
            var patient = patientRepo.GetAll().FirstOrDefault(x => x.UserName == "testUsername");
            var response = await Client.GetAsync(BaseUrl + "api/BlockPatient/GetMaliciousPatients");
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePatients = JsonConvert.DeserializeObject<List<Patient>>(responseContent);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            foreach (var p in responsePatients.Where(p => p.UserName.Equals("testUsername")))
            {
                isMalicious = true;
            }
            isMalicious.ShouldBe(false);
            ClearDatabase();
        }

        private void ArrangeDataForGetMaliciousTrue()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetAll()
               .FirstOrDefault(x => x.Name == "TestCountry");
            if (country == null)
            {
                country = new Country()
                {
                    Name = "TestCountry",
                };
                UoW.GetRepository<ICountryWriteRepository>().Add(country);
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
            if (city == null)
            {
                city = new City()
                {
                    Name = "TestCity",
                    PostalCode = 00000,
                    Country = country

                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);
            }

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

            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization == null)
            {
                specialization = new Specialization()
                {
                    Description = "DescriptionSpecialization",
                    Name = "TestSpecialization"
                };
                UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);
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
                    SpecializationId = specialization.Id,
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Room = room,
                    City = city

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername");

            if (patient == null)
            {
                var medicalRecord = new MedicalRecord
                {
                    Weight = 70,
                    Height = 168,
                    BloodType = BloodType.ABNegative,
                    JobStatus = JobStatus.Student,
                    Doctor = doctor

                };
                patient = new Patient()
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
                    MedicalRecord = medicalRecord,
                    City = city,
                    IsBlocked = false

                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == doctor.Id);
                if (scheduledEvent == null)
                {
                    scheduledEvent = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-100),
                        EndDate = DateTime.Now.AddDays(-100),
                        CancellationDate = DateTime.Now.AddDays(-110),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);

                    var scheduledEvent2 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-22),
                        EndDate = DateTime.Now.AddDays(-22),
                        CancellationDate = DateTime.Now.AddDays(-27),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
                    var scheduledEvent3 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-4),
                        EndDate = DateTime.Now.AddDays(-4),
                        CancellationDate = DateTime.Now.AddDays(-14),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent3);
                    var scheduledEvent4 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(7),
                        EndDate = DateTime.Now.AddDays(7),
                        CancellationDate = DateTime.Now.AddDays(-1),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent4);
                }
            }
        }
        private void ArrangeDataForGetMaliciousFalse()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetAll()
               .FirstOrDefault(x => x.Name == "TestCountry");
            if (country == null)
            {
                country = new Country()
                {
                    Name = "TestCountry",
                };
                UoW.GetRepository<ICountryWriteRepository>().Add(country);
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
            if (city == null)
            {
                city = new City()
                {
                    Name = "TestCity",
                    PostalCode = 00000,
                    Country = country

                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);
            }

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

            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization == null)
            {
                specialization = new Specialization()
                {
                    Description = "DescriptionSpecialization",
                    Name = "TestSpecialization"
                };
                UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);
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
                    SpecializationId = specialization.Id,
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Room = room,
                    City = city

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername");

            if (patient == null)
            {
                var medicalRecord = new MedicalRecord
                {
                    Weight = 70,
                    Height = 168,
                    BloodType = BloodType.ABNegative,
                    JobStatus = JobStatus.Student,
                    Doctor = doctor

                };
                patient = new Patient()
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
                    MedicalRecord = medicalRecord,
                    City = city,
                    IsBlocked = false

                };
                UoW.GetRepository<IPatientWriteRepository>().Add(patient);

                var date = new DateTime();
                var scheduledEvent = UoW.GetRepository<IScheduledEventReadRepository>().GetAll()
                    .FirstOrDefault(x => x.StartDate == date && x.Patient.Id == patient.Id && x.Doctor.Id == doctor.Id);
                if (scheduledEvent == null)
                {
                    scheduledEvent = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-100),
                        EndDate = DateTime.Now.AddDays(-100),
                        CancellationDate = DateTime.Now.AddDays(-110),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);

                    var scheduledEvent2 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-22),
                        EndDate = DateTime.Now.AddDays(-22),
                        CancellationDate = DateTime.Now.AddDays(-27),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent2);
                    var scheduledEvent3 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(-4),
                        EndDate = DateTime.Now.AddDays(-4),
                        Doctor = doctor,
                        IsCanceled = false,
                        IsDone = true,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent3);
                    var scheduledEvent4 = new ScheduledEvent()
                    {
                        StartDate = DateTime.Now.AddDays(7),
                        EndDate = DateTime.Now.AddDays(7),
                        CancellationDate = DateTime.Now.AddDays(-1),
                        Doctor = doctor,
                        IsCanceled = true,
                        IsDone = false,
                        Patient = patient,
                        Room = doctor.Room,
                        ScheduledEventType = ScheduledEventType.Appointment
                    };
                    UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent4);
                }
            }
        }


        private void ArrangeDatabase()
        {
            var country = UoW.GetRepository<ICountryReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestCountry");
            if (country == null)
            {
                country = new Country()
                {
                    Name = "TestCountry",
                };
                UoW.GetRepository<ICountryWriteRepository>().Add(country);
            }

            var city = UoW.GetRepository<ICityReadRepository>().GetAll().FirstOrDefault(x => x.Name == "TestCity");
            if (city == null)
            {
                city = new City()
                {
                    Name = "TestCity",
                    PostalCode = 00000,
                    Country = country

                };
                UoW.GetRepository<ICityWriteRepository>().Add(city);
            }

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

            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization == null)
            {
                specialization = new Specialization()
                {
                    Description = "DescriptionSpecialization",
                    Name = "TestSpecialization"
                };
                UoW.GetRepository<ISpecializationWriteRepository>().Add(specialization);
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
                    SpecializationId = specialization.Id,
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    Room = room,
                    City = city

                };
                UoW.GetRepository<IDoctorWriteRepository>().Add(doctor);
            }

            var patient = UoW.GetRepository<IPatientReadRepository>().GetAll()
                .FirstOrDefault(x => x.UserName == "testUsername");

            if (patient == null)
            {
                var medicalRecord = new MedicalRecord
                {
                    Weight = 70,
                    Height = 168,
                    BloodType = BloodType.ABNegative,
                    JobStatus = JobStatus.Student,
                    Doctor = doctor

                };
                patient = new Patient()
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
                    MedicalRecord = medicalRecord,
                    City = city,
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
            var specialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll()
                .FirstOrDefault(x => x.Name == "TestSpecialization");
            if (specialization != null)
            {
                UoW.GetRepository<ISpecializationWriteRepository>().Delete(specialization);
            }
            var city = UoW.GetRepository<ICityReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCity");

            if (city != null)
            {
                UoW.GetRepository<ICityWriteRepository>().Delete(city);
            }

            var country = UoW.GetRepository<ICountryReadRepository>()
                .GetAll().ToList()
                .FirstOrDefault(x => x.Name == "TestCountry");

            if (country != null)
            {
                UoW.GetRepository<ICountryWriteRepository>().Delete(country);
            }

            var room = UoW.GetRepository<IRoomReadRepository>()
                    .GetAll().ToList()
                    .FirstOrDefault(x => x.Name == "TestRoom");

            if (room != null)
            {
                UoW.GetRepository<IRoomWriteRepository>().Delete(room);
            }
        }
    }
}
