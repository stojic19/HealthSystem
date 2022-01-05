using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using HospitalIntegrationTests.Base;
using System;
using Hospital.SharedModel.Model.Enumerations;
using System.Threading.Tasks;
using Xunit;
using Hospital.RoomsAndEquipment.Model;
using Shouldly;
using System.Net;
using Hospital.SharedModel.Repository;
using Hospital.MedicalRecords.Repository;
using System.Linq;
using Hospital.RoomsAndEquipment.Repository;

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
           var events = AddDataToDatabase(isCanceled: false, isDone: true);
           var response = await Client.GetAsync(BaseUrl + "api/ScheduledEvent/GetFinishedUserEvents/" + events.PatientId);
           response.StatusCode.ShouldBe(HttpStatusCode.OK);

            UoW.GetRepository<IScheduledEventReadRepository>().GetFinishedUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            DeleteDataFromDataBase(events);
        }

       [Fact]
        public async Task Canceled_events_count_should_not_be_zero()
        {
            var events = AddDataToDatabase(isCanceled: true, isDone: false);
            var response = await Client.GetAsync(BaseUrl + "api/ScheduledEvent/GetCanceledUserEvents/" + events.Patient.Id);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            UoW.GetRepository<IScheduledEventReadRepository>().GetCanceledUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            DeleteDataFromDataBase(events);
        }
        [Fact]
        public async Task Upcoming_events_count_should_not_be_zero()
        {
            var events = AddDataToDatabase(isCanceled: false, isDone: false);
            var response = await Client.GetAsync(BaseUrl + "api/ScheduledEvent/GetUpcomingUserEvents/" + events.Patient.Id);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            UoW.GetRepository<IScheduledEventReadRepository>().GetUpcomingUserEvents(events.Patient.UserName).Count.ShouldNotBe(0);
            DeleteDataFromDataBase(events);
        }
        private void DeleteDataFromDataBase(ScheduledEvent events)
        {
            UoW.GetRepository<IScheduledEventWriteRepository>().Delete(events);

        }

        private ScheduledEvent AddDataToDatabase(bool isCanceled, bool isDone)
        {
            var countryWriteRepo = UoW.GetRepository<ICountryWriteRepository>();
            var cityWriteRepo = UoW.GetRepository<ICityWriteRepository>();
            var roomWriteRepo = UoW.GetRepository<IRoomWriteRepository>();
            var specializationWriteRepo = UoW.GetRepository<ISpecializationWriteRepository>();
            var doctorWiteRepo = UoW.GetRepository<IDoctorWriteRepository>();
            var patientWriteRepo = UoW.GetRepository<IPatientWriteRepository>();

            var testCountry = UoW.GetRepository<ICountryReadRepository>().GetAll().Where(x => x.Name == "TestCountry").FirstOrDefault();
            var testCity = UoW.GetRepository<ICityReadRepository>().GetAll().Where(x => x.Name == "TestCity").FirstOrDefault();
            var testRoom = UoW.GetRepository<IRoomReadRepository>().GetAll().Where(x => x.Name == "TestRoom").FirstOrDefault();
            var testSpecialization = UoW.GetRepository<ISpecializationReadRepository>().GetAll().Where(x => x.Name == "TestSpecialization").FirstOrDefault();
            var testDoctor = UoW.GetRepository<IDoctorReadRepository>().GetAll().Where(x => x.FirstName == "TestDoctor").FirstOrDefault();
            var testPatient = UoW.GetRepository<IPatientReadRepository>().GetAll().Where(x => x.FirstName == "TestPatient").FirstOrDefault();

            if (testCountry == null)
            {
                testCountry = new Country()
                {
                    Name = "TestCountry"
                };
                countryWriteRepo.Add(testCountry);
            }

            if (testCity == null)
            {
                testCity = new City()
                {
                    Name = "TestCity",
                    Country = testCountry,
                    PostalCode = 00000
                };
                cityWriteRepo.Add(testCity);
            }
            if (testRoom == null)
            {
                testRoom = new Room()
                {
                    Name = "TestRoom",
                    RoomType = RoomType.AppointmentRoom,
                    FloorNumber = 1,
                    Description = "TestDescription"

                };
                roomWriteRepo.Add(testRoom);
            }
            if (testSpecialization == null)
            {
                testSpecialization = new Specialization()
                {
                    Description = "DescriptionSpecialization",
                    Name = "TestSpecialization"
                };
                specializationWriteRepo.Add(testSpecialization);
            }
            if (testDoctor == null)
            {
                testDoctor = new Doctor()
                {
                    FirstName = "TestDoctor",
                    LastName = "TestDoctorLastName",
                    MiddleName = "TestDoctorMiddleName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TestDoctorStreet",
                    City = testCity,
                    Room = testRoom,
                    Specialization = testSpecialization,
                    UserName = "testDoctorUsername",
                    Email = "testDoctor@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testDoctorPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0

                };
                doctorWiteRepo.Add(testDoctor);
            }
            if (testPatient == null)
            {
                MedicalRecord medicalRecord = new MedicalRecord
                {
                    Doctor = testDoctor,
                    Height = 0.0,
                    Weight = 0.0,
                    BloodType = BloodType.ABNegative,
                    JobStatus = JobStatus.Student

                };

                testPatient = new Patient()
                {
                    FirstName = "TestPatient",
                    MiddleName = "TestPatientMiddleName",
                    LastName = "TestPatientLastName",
                    DateOfBirth = DateTime.Now,
                    Gender = Gender.Female,
                    Street = "TesPatientStreet",
                    City = testCity,
                    IsBlocked = false,
                    UserName = "testPatientrUsername",
                    Email = "testPatient@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "testPatientPhoneNumber",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    MedicalRecord = medicalRecord

                };
                patientWriteRepo.Add(testPatient);
            }
            ScheduledEvent scheduledEvent = new ScheduledEvent()
            {

                ScheduledEventType = 0,
                IsCanceled = isCanceled,
                IsDone = isDone,
                StartDate = new DateTime(2021, 10, 17),
                EndDate = new DateTime(2021, 10, 17),
                Patient = testPatient,
                Doctor = testDoctor,
                Room = testRoom


            };

            UoW.GetRepository<IScheduledEventWriteRepository>().Add(scheduledEvent);
            return scheduledEvent;
        }
    }
}